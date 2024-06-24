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

resource "azurerm_resource_group" "instance" {
  name     = "${var.name}-${lower(var.environment)}-rg"
  location = var.location
}

output "resource_group_name" {
  value       = azurerm_resource_group.instance.name
  sensitive   = false
}

output "web_app_api_name" {
  value       = azurerm_windows_web_app.api.name
  sensitive   = false
}

output "web_app_api_hostname" {
  value       = azurerm_windows_web_app.api.default_hostname
  sensitive   = false
}

output "web_app_website_name" {
  value       = azurerm_windows_web_app.website.name
  sensitive   = false
}

output "web_app_website_hostname" {
  value       = azurerm_windows_web_app.website.default_hostname
  sensitive   = false
}

output "container_app_api_fqdn" {
  value       = azurerm_container_app.api.latest_revision_fqdn
  sensitive   = false
}

output "container_instance_monitoring_fqdn" {
  value       = one(azurerm_container_group.monitoring[*].fqdn)
  sensitive   = false
}
