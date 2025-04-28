use config::{Config, ConfigError};
use serde::Deserialize;

#[derive(Debug, Deserialize)]
pub struct Settings {
    pub port: u16,
}

impl Settings {
    pub fn new() -> Result<Self, ConfigError> {
        let settings = Config::builder()
            .add_source(config::File::with_name("rockpal-chat/src/Settings"))
            .add_source(config::Environment::with_prefix("APP"))
            .build()?;

        let config: Settings = settings.try_deserialize()?;

        Ok(config)
    }
}
