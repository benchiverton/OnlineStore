use base64::Engine;
use base64::engine::general_purpose;
use futures_util::{SinkExt, StreamExt};
use sha1::{Digest, Sha1};
use std::io::Read;
use std::net::SocketAddr;
use std::{io::Error as IoError, str::Utf8Error, time::Duration};
use thiserror::Error;
use tokio::io::AsyncReadExt;
use tokio::net::TcpStream;
use tokio::{io::AsyncWriteExt, net::TcpListener, time::sleep};
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
    #[error("WebSocket upgrade failed: {0}")]
    WebSocketUpgradeError(String),
}

pub struct WebSocketServer {
    address: String,
    port: u16,
}

impl WebSocketServer {
    pub fn new(address: String, port: u16) -> Self {
        Self { address, port }
    }

    pub async fn start(self, token: CancellationToken) -> Result<(), Error> {
        tracing::info!("Starting server on {}:{}", self.address, self.port);

        let listener = TcpListener::bind(format!("{}:{}", self.address, self.port)).await?;

        loop {
            tokio::select! {
                _ = token.cancelled() => {
                    tracing::info!("Server is shutting down.");
                    break;
                },
                Ok((stream, addr)) = listener.accept() => {
                    tracing::info!(?addr, "Accepted new TCP connection");

                    tokio::spawn(async move {
                        if let Err(e) = handle_connection(stream, &addr).await {
                            tracing::error!(?addr, "Connection error: {:?}", e);
                        }
                    });
                }
            }
        }

        Ok(())
    }
}

async fn handle_connection(mut stream: TcpStream, addr: &SocketAddr) -> Result<(), Error> {
    let mut buffer = [0u8; 1024];
    let bytes_read = stream.read(&mut buffer).await?;
    let request = String::from_utf8_lossy(&buffer[..bytes_read]).into_owned();

    if request.contains("upgrade: websocket") && request.contains("connection: Upgrade") {
        let accept_key = generate_accept_key(&request)?;

        tracing::info!(?addr, "Connection upgrade started.");
        let response = format!(
            "HTTP/1.1 101 Switching Protocols\r\n\
                            Upgrade: websocket\r\n\
                            Connection: Upgrade\r\n\
                            Sec-WebSocket-Accept: {}\r\n\r\n",
            accept_key
        );
        stream.write_all(response.as_bytes()).await?;
        tracing::info!(?addr, "Connection upgrade completed.");

        let mut ws_stream = ServerBuilder::new().serve(stream);

        while let Some(Ok(msg)) = ws_stream.next().await {
            tracing::info!(?addr, "Received: {:?}", msg);

            sleep(Duration::from_secs(1)).await;

            if msg.is_text() || msg.is_binary() {
                let byte_vec: Vec<u8> = msg.into_payload().bytes().filter_map(Result::ok).collect();
                let rockpal_name = std::str::from_utf8(&byte_vec)?;
                let message = Message::text(format!(
                    "Your RockPal does not understand the phrase '{}'. They are, after all, a rock.",
                    rockpal_name
                ));
                ws_stream.send(message).await?;
            }
        }
    } else {
        tracing::warn!(
            ?addr,
            request,
            "Unsupported request, expected websocket/connection upgrade. Shutting down stream."
        );
        stream.shutdown().await?;
    }

    Ok(())
}

fn generate_accept_key(request: &String) -> Result<String, Error> {
    let websocket_key_line = request
        .lines()
        .find(|line| line.to_lowercase().starts_with("sec-websocket-key:"))
        .ok_or(Error::WebSocketUpgradeError(
            "Missing Sec-WebSocket-Key".into(),
        ))?;
    let websocket_key = websocket_key_line
        .split(':')
        .nth(1)
        .ok_or(Error::WebSocketUpgradeError(
            "Malformed Sec-WebSocket-Key header".into(),
        ))?
        .trim();
    let mut hasher = Sha1::new();
    hasher.update(format!("{}258EAFA5-E914-47DA-95CA-C5AB0DC85B11", websocket_key).as_bytes());
    let accept_key = hasher.finalize();

    Ok(general_purpose::STANDARD.encode(&accept_key))
}
