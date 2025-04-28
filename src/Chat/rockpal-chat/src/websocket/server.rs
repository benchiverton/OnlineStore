use futures_util::{SinkExt, StreamExt, TryFutureExt};
use std::io::Error as IoError;
use thiserror::Error;
use tokio::net::TcpListener;
use tokio_util::sync::CancellationToken;
use tokio_websockets::{Error as WebSocketError, Message, ServerBuilder};

#[derive(Debug, Error)]
pub enum Error {
    #[error(transparent)]
    Io(#[from] IoError),
    #[error(transparent)]
    WebSocket(#[from] WebSocketError),
}

pub struct WebSocketServer {
    port: u32,
}

impl WebSocketServer {
    pub fn new(port: u32) -> Self {
        Self { port }
    }

    pub async fn start(self, token: CancellationToken) -> Result<(), Error> {
        tracing::info!("Starting server...");

        let listener = TcpListener::bind(format!("127.0.0.1:{}", self.port))
            .await
            .map_err(Error::from)?;

        loop {
            tokio::select! {
                _ = token.cancelled() => {
                    tracing::info!("Server is shutting down...");
                    break;
                },
                Ok((stream, addr)) = listener.accept() => {

                    tracing::info!(address = ?addr, "New connection accepted.");

                    let result = ServerBuilder::new()
                        .accept(stream)
                        .map_err(Error::from)
                        .await;

                    match result {
                        Ok((_request, mut ws_stream)) => {
                            tokio::spawn(async move {
                                while let Some(Ok(msg)) = ws_stream.next().await {
                                    tracing::info!(message = ?msg, "Message received.");
                                    if msg.is_text() || msg.is_binary() {
                                        ws_stream.send(Message::text("Your RockPal does not respond. It is, after all, a rock.")).await?;
                                    }
                                }
                                Ok::<_, Error>(())
                            });
                        }
                        Err(e) => tracing::error!("Error accepting websocket connection: {:?}", e),
                    }
                }
            }
        }

        Ok(())
    }
}
