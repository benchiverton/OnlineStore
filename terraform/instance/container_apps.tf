resource  "azurerm_container_app_environment" "apps" {
  name                = "${var.name}-containerapps"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
}

resource "azurerm_container_app" "api" {
  name                         = "api"
  container_app_environment_id = azurerm_container_app_environment.apps.id
  resource_group_name          = azurerm_resource_group.isntance.name
  revision_mode                = "Single"

  template {
    container {
      name   = "api"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.25
      memory = "0.5Gi"
    }
  }
}
