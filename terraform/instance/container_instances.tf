resource "azurerm_container_group" "jaeger" {
  name                = "${var.name}-containergroup-jaeger"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  ip_address_type     = "Public"
  os_type             = "Linux"

  container {
    name   = "jaegertracing-all-in-one"
    image  = "jaegertracing/all-in-one:1.42"
    cpu    = "0.5"
    memory = "1.5"

    # serve frontend
    ports {
      port     = 16686
      protocol = "TCP"
    }
    # OTLP over HTTP
    ports {
      port     = 4317
      protocol = "TCP"
    }

    environment_variables = {
      COLLECTOR_ZIPKIN_HOST_PORT = ":9411"
      COLLECTOR_OTLP_ENABLED     = "true"
    }
  }

  tags = {
    environment = var.environment
  }
}
