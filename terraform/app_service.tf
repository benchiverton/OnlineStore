resource "azurerm_app_service_plan" "default" {
  name                = "${var.name}-plan"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  kind                = "Linux"

  # Reserved must be set to true for Linux App Service Plans
  reserved = true

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
    # always_on = false
    linux_fx_version          = "DOTNETCORE|6.0"
    use_32_bit_worker_process = true
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

  identity {
    type = "SystemAssigned"
  }

  site_config {
    # always_on = false
    linux_fx_version          = "DOTNETCORE|6.0"
    use_32_bit_worker_process = true
  }

  app_settings = {
    // environment variables for service
  }
}