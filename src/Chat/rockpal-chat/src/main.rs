use std::env;

use config::ConfigError;
use settings::settings::Settings;
use thiserror::Error;
use tokio::{signal, task::JoinError};
use tokio_util::sync::CancellationToken;
use tracing_subscriber;
use websocket::server::{Error as WebSocketError, WebSocketServer};

mod settings;
mod websocket;

#[derive(Debug, Error)]
pub enum Error {
    #[error(transparent)]
    WebSocket(#[from] WebSocketError),
    #[error(transparent)]
    JoinError(#[from] JoinError),
    #[error(transparent)]
    ConfigError(#[from] ConfigError),
}

#[tokio::main]
async fn main() -> Result<(), Error> {
    let args: Vec<String> = env::args().collect();
    let settings_file = &args[1];
    let settings = Settings::new(settings_file)?;

    tracing_subscriber::fmt::init();

    let token = CancellationToken::new();

    let server = WebSocketServer::new(settings.port);
    let server_token = token.clone();
    let handle = tokio::spawn(async move { server.start(server_token) });

    tokio::spawn({
        async move {
            signal::ctrl_c()
                .await
                .expect("Failed to listen for CTRL_C_EVENT");

            tracing::info!("Cancellation event received, shutting down.");

            token.cancel();
        }
    });

    handle.await.map_err(Error::from)?.await?;

    Ok(())
}
