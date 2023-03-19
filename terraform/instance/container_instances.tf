resource "azurerm_container_group" "monitoring" {
  name                = "${var.name}-containergroup-monitoring"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  ip_address_type     = "Public"
  dns_name_label      = "${var.name}-${lower(var.environment)}-monitoring"
  os_type             = "Linux"

  container {
    name   = "jaegertracing-all-in-one"
    image  = "onlinestorecontainerregistry.azurecr.io/jaegertracing/all-in-one:1.42"
    cpu    = "0.05"
    memory = "0.20"

    # serve frontend
    ports {
      port     = 16686
      protocol = "TCP"
    }
    # OTLP over HTTP
    ports {
      port     = 4318
      protocol = "TCP"
    }

    environment_variables = {
      COLLECTOR_ZIPKIN_HOST_PORT = ":9411"
      COLLECTOR_OTLP_ENABLED     = "true"
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
