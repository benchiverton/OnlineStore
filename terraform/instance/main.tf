terraform {
  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "=1.13.1"
    }
    azuread = {
      source = "hashicorp/azuread"
      version = "=2.53.1"
    }
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
