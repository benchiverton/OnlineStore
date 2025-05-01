use config::{Config, ConfigError};
use serde::Deserialize;

#[derive(Debug, Deserialize)]
pub struct Settings {
    pub port: u16,
}

impl Settings {
    pub fn new(settings_file: &String) -> Result<Self, ConfigError> {
        let settings_builder = Config::builder()
            .add_source(config::File::with_name(settings_file))
            .add_source(config::Environment::with_prefix("APP"))
            .build()?;

        let settings: Settings = settings_builder.try_deserialize()?;

        Ok(settings)
    }
}
