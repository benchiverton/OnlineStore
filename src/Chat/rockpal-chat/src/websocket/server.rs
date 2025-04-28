use futures_util::{SinkExt, StreamExt, TryFutureExt};
use std::{
    io::{Error as IoError, Read},
    str::Utf8Error,
    time::Duration,
};
use thiserror::Error;
use tokio::{net::TcpListener, time::sleep};
use tokio_util::sync::CancellationToken;
use tokio_websockets::{Error as WebSocketError, Message, ServerBuilder};

#[derive(Debug, Error)]
pub enum Error {
    #[error(transparent)]
    Io(#[from] IoError),
    #[error(transparent)]
    WebSocket(#[from] WebSocketError),
    #[error(transparent)]
    Utf8Error(#[from] Utf8Error),
}

pub struct WebSocketServer {
    port: u32,
}

impl WebSocketServer {
    pub fn new(port: u32) -> Self {
        Self { port }
    }

    pub async fn start(self, token: CancellationToken) -> Result<(), Error> {
        tracing::info!("Starting server.");

        let listener = TcpListener::bind(format!("127.0.0.1:{}", self.port))
            .await
            .map_err(Error::from)?;

        loop {
            tokio::select! {
                _ = token.cancelled() => {
                    tracing::info!("Server is shutting down.");
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

                                    // simulate thinking...
                                    sleep(Duration::from_secs(1)).await;

                                    if msg.is_text() || msg.is_binary() {
                                        let byte_vec: Vec<u8> = msg
                                            .into_payload()
                                            .bytes()
                                            .filter_map(Result::ok)
                                            .collect();
                                        let rockpal_name = std::str::from_utf8(&byte_vec)?;
                                        let message = Message::text(format!("Your RockPal does not understand the phrase '{}'. They are, after all, a rock.", rockpal_name));
                                        ws_stream.send(message).await?;
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
