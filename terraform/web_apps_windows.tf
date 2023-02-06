resource "azurerm_service_plan" "windows" {
  name                = "${var.name}-plan-windows"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  sku_name            = "var.plan_sku"
  os_type             = "Windows"

  # Reserved must be set to false for Windows App Service Plans
  reserved = false
}

resource "azurerm_windows_web_app" "api" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-api"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  app_service_plan_id = azurerm_app_service_plan.windows.id

  identity {
    type = "SystemAssigned"
  }

  site_config {
    dotnet_framework_version  = "v6.0"
    default_documents         = ["index.html"] // swagger
    always_on                 = false          // free tier
    use_32_bit_worker_process = true           // free tier
  }

  app_settings = {
    // environment variables for service
  }
}

resource "azurerm_windows_web_app" "website" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-website"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  app_service_plan_id = azurerm_app_service_plan.windows.id

  # Should the app service send session affinity cookies?
  client_affinity_enabled = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    dotnet_framework_version  = "v6.0"
    default_documents         = ["index.html"]
    always_on                 = false // free tier
    use_32_bit_worker_process = true  // free tier
  }

  app_settings = {
    // environment variables for service
  }
}
