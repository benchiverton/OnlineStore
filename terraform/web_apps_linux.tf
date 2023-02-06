resource "azurerm_service_plan" "linux" {
  name                = "${var.name}-plan-linux"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  os_type             = "Linux"
  sku_name            = var.plan_sku
}

resource "azurerm_linux_web_app" "jaeger" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-jaeger"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  service_plan_id     = azurerm_service_plan.linux.id

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on = false // free tier
    application_stack {
      docker_image     = "jaegertracing/all-in-one"
      docker_image_tag = "1.42"
    }
  }

  app_settings = {
    "COLLECTOR_OTLP_ENABLED"     = "true",
    "COLLECTOR_ZIPKIN_HOST_PORT" = ":9411"
  }
}
