data  "azurerm_container_app_environment" "apps" {
  name                = "${var.name}-containerapps"
  resource_group_name = "onlinestore-shared-rg"
}

resource "azurerm_container_app" "api" {
  name                         = "${lower(var.environment)}-${var.name}-api"
  container_app_environment_id = data.azurerm_container_app_environment.apps.id
  resource_group_name          = data.azurerm_container_app_environment.apps.resource_group_name
  revision_mode                = "Single"

  template {
    container {
      name   = "${lower(var.environment)}-onlinestore-api"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.25
      memory = "0.5Gi"
    }
    max_replicas = 1
  }

  ingress {
    external_enabled = true
    transport        = "http"
    target_port      = 8080
    traffic_weight {
      latest_revision = true
      percentage = 100
    }
  }
  
  lifecycle {
    ignore_changes = [
      template[0].container[0],
      registry,
      secret,
    ]
  }
}

resource "azurerm_container_app" "website" {
  name                         = "${lower(var.environment)}-${var.name}-website"
  container_app_environment_id = data.azurerm_container_app_environment.apps.id
  resource_group_name          = data.azurerm_container_app_environment.apps.resource_group_name
  revision_mode                = "Single"

  template {
    container {
      name   = "${lower(var.environment)}-onlinestore-website"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.25
      memory = "0.5Gi"
    }
    max_replicas = 1
  }

  ingress {
    external_enabled = true
    transport        = "http"
    target_port      = 80
    traffic_weight {
      latest_revision = true
      percentage = 100
    }
  }
  
  lifecycle {
    ignore_changes = [
      template[0].container[0],
      registry,
      secret,
    ]
  }
}

resource "azurerm_container_app" "monitoring" {
  name                         = "${lower(var.environment)}-${var.name}-monitoring"
  container_app_environment_id = data.azurerm_container_app_environment.apps.id
  resource_group_name          = data.azurerm_container_app_environment.apps.resource_group_name
  revision_mode                = "Single"

  template {
    container {
      name   = "${lower(var.environment)}-aspire-dashboard"
      image  = "onlinestorecontainerregistry.azurecr.io/dotnet/aspire-dashboard:9.0.0"
      cpu    = 0.25
      memory = "0.5Gi"
      env {
        name  = "DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS"
        value = "true"
      }
    }
    max_replicas = 1
  }

  ingress {
    external_enabled = true
    transport        = "http"
    target_port      = 18888
    traffic_weight {
      latest_revision = true
      percentage = 100
    }
  }
  
  registry {
    server   = "onlinestorecontainerregistry.azurecr.io"
    username = var.acr_username
    password_secret_name = "acr-password"
  }

  secret {
    name = "acr-password"
    value = var.acr_password
  }
}

# update the container app with extra additionalPortMappings, as this is not supported by the existing TF provider
resource "azapi_update_resource" "monitoring_portmappings" {
  type        = "Microsoft.App/containerApps@2023-11-02-preview"
  resource_id = azurerm_container_app.monitoring.id

  body = jsonencode({
    properties = {
      configuration = {
        secrets = [{
          name = "acr-password"
          value = var.acr_password
        }]
        ingress = {
          additionalPortMappings = [{
            exposedPort = 18889,
            targetPort = 18889,
            external = false
          }]
        }
      }
    }
  })

  depends_on = [
    azurerm_container_app.monitoring,
  ]

  lifecycle {
    replace_triggered_by = [azurerm_container_app.monitoring]
  }
}
