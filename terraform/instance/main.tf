terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.109.0"
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

output "container_app_api_fqdn" {
  value       = azurerm_container_app.api.ingress[0].fqdn
  sensitive   = false
}

output "container_app_website_fqdn" {
  value       = azurerm_container_app.website.ingress[0].fqdn
  sensitive   = false
}

output "container_instance_monitoring_fqdn" {
  value       = one(azurerm_container_group.monitoring[*].fqdn)
  sensitive   = false
}
