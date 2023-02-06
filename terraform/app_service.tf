resource "azurerm_app_service_plan" "windows" {
  name                = "${var.name}-plan-windows"
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

resource "azurerm_app_service_plan" "linux" {
  name                = "${var.name}-plan-linux"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  kind                = "Linux"

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

resource "azurerm_app_service" "website" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-website"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
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

resource "azurerm_app_service" "jaeger" {
  name                = "${var.dns_prefix}-${var.name}-${var.environment}-jaeger"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  app_service_plan_id = azurerm_app_service_plan.windows.id

  client_affinity_enabled = false

  site_config {
    always_on        = false // free tier
    linux_fx_version = "DOCKER|jaegertracing/all-in-one:1.42"
  }

  identity {
    type = "SystemAssigned"
  }

  app_settings = {
    "COLLECTOR_OTLP_ENABLED" = "true",
    "COLLECTOR_ZIPKIN_HOST_PORT" = ":9411"
  }
}
