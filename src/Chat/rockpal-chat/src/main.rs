use thiserror::Error;
use tokio::{signal, task::JoinError};
use tokio_util::sync::CancellationToken;
use tracing_subscriber;
use websocket::server::{Error as WebSocketError, WebSocketServer};

mod websocket;

#[derive(Debug, Error)]
pub enum Error {
    #[error(transparent)]
    WebSocket(#[from] WebSocketError),
    #[error(transparent)]
    JoinError(#[from] JoinError),
}

#[tokio::main]
async fn main() -> Result<(), Error> {
    tracing_subscriber::fmt::init();

    let token = CancellationToken::new();

    let server = WebSocketServer::new(11000);
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
