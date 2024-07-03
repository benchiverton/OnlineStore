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

resource "azurerm_resource_group" "permanent" {
  name     = "${var.name}-permanent-rg"
  location = var.location
}
