resource  "azurerm_container_app_environment" "apps" {
  name                = "${var.name}-containerapps"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
}

resource "azurerm_container_app" "api" {
  name                         = "${var.name}-api"
  container_app_environment_id = azurerm_container_app_environment.apps.id
  resource_group_name          = azurerm_resource_group.instance.name
  revision_mode                = "Single"

  template {
    container {
      name   = "api"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.25
      memory = "0.5Gi"
    }
  }

  ingress {
    external_enabled = true
    target_port      = 80
    traffic_weight {
      latest_revision = true
      percentage = 100
    }
  }
}
