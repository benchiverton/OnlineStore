resource "azurerm_service_plan" "windows" {
  name                = "${var.name}-plan-windows"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  os_type             = "Windows"
  sku_name            = var.plan_sku
}

resource "azurerm_windows_web_app" "api" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-api"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  service_plan_id     = azurerm_service_plan.windows.id

  identity {
    type = "SystemAssigned"
  }

  site_config {
    default_documents = ["index.html"] // swagger
    always_on         = false          // free tier
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v6.0"
    }
  }

  app_settings = {
    OTLPEXPORTER__ENDPOINT = "http://${azurerm_container_group.monitoring.fqdn}:4318"
  }
}

resource "azurerm_windows_web_app" "website" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-website"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
  service_plan_id     = azurerm_service_plan.windows.id

  # Should the app service send session affinity cookies?
  client_affinity_enabled = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    default_documents = ["index.html"]
    always_on         = false // free tier
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v6.0"
    }
  }

  app_settings = {
    // environment variables for service
  }
}
