resource "azurerm_app_service_plan" "default" {
  name                = "${var.name}-plan"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  kind                = "Windows"

  # Reserved must be set to false for Windows App Service Plans
  reserved = false

  sku {
    tier = var.plan_tier
    size = var.plan_sku
  }
}

resource "azurerm_app_service" "api" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-api"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  app_service_plan_id = azurerm_app_service_plan.default.id

  identity {
    type = "SystemAssigned"
  }

  site_config {
    dotnet_framework_version  = "v6.0"
    always_on = false // free tier
    use_32_bit_worker_process = true // free tier
  }

  app_settings = {
    // environment variables for service
  }
}

resource "azurerm_app_service" "website" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-website"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  app_service_plan_id = azurerm_app_service_plan.default.id

  # Should the app service send session affinity cookies?
  client_affinity_enabled = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    dotnet_framework_version  = "v6.0"
    default_documents = "index.html"
    always_on = false // free tier
    use_32_bit_worker_process = true // free tier
  }

  app_settings = {
    // environment variables for service
  }
}
