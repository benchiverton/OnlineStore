terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.42.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "onlinestoretfstate"
    storage_account_name = "onlinestoretfstate"
    container_name       = "onlinestoretfstate"
    key                  = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "default" {
  name     = "${var.name}-${lower(var.environment)}-rg"
  location = var.location
}

output "app_service_name_api" {
  value       = azurerm_windows_web_app.api.name
  description = "Online Store API app."
  sensitive   = false
}

output "app_service_name_website" {
  value       = azurerm_windows_web_app.website.name
  description = "Online Store website app."
  sensitive   = false
}
