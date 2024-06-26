resource "azurerm_container_group" "monitoring" {
  count               = var.monitoring_enabled ? 1 : 0
  name                = "${var.name}-containergroup-monitoring"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  ip_address_type     = "Public"
  dns_name_label      = "${var.name}-${lower(var.environment)}-monitoring"
  os_type             = "Linux"

  container {
    name   = "aspire-dashboard"
    image  = "onlinestorecontainerregistry.azurecr.io/dotnet/aspire-dashboard:8.0.0"
    cpu    = "0.05"
    memory = "0.20"

    # serve frontend
    ports {
      port     = 18888
      protocol = "TCP"
    }
    # OTLP over GRPC
    ports {
      port     = 18889
      protocol = "TCP"
    }

    environment_variables = {
      DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS = "true"
    }
  }

  image_registry_credential {
    server   = "onlinestorecontainerregistry.azurecr.io"
    username = var.acr_username
    password = var.acr_password
  }

  tags = {
    environment = var.environment
  }
}
