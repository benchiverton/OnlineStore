# resource "azurerm_app_service_plan" "linux" {
#   name                = "${var.name}-plan-linux"
#   location            = azurerm_resource_group.default.location
#   resource_group_name = azurerm_resource_group.default.name
#   kind                = "Linux"

#   reserved = true

#   sku {
#     tier = var.plan_tier
#     size = var.plan_sku
#   }
# }

# resource "azurerm_app_service" "jaeger" {
#   name                = "${var.dns_prefix}-${var.name}-${var.environment}-jaeger"
#   location            = azurerm_resource_group.default.location
#   resource_group_name = azurerm_resource_group.default.name
#   app_service_plan_id = azurerm_app_service_plan.linux.id

#   site_config {
#     always_on        = false // free tier
#     linux_fx_version = "DOCKER|jaegertracing/all-in-one:1.42"
#   }

#   identity {
#     type = "SystemAssigned"
#   }

#   app_settings = {
#     "COLLECTOR_OTLP_ENABLED" = "true",
#     "COLLECTOR_ZIPKIN_HOST_PORT" = ":9411"
#   }
# }
