resource  "azurerm_container_app_environment" "apps" {
  name                = "${var.name}-containerapps"
  resource_group_name = azurerm_resource_group.instance.name
  location            = azurerm_resource_group.instance.location
}

resource "azurerm_container_app" "api" {
  name                         = "${var.name}-api"
  container_app_environment_id = azurerm_container_app_environment.apps.id
  resource_group_name          = azurerm_resource_group.instance.name
  revision_mode                = "Single"

  template {
    container {
      name   = "onlinestore-api"
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
  name                         = "${var.name}-website"
  container_app_environment_id = azurerm_container_app_environment.apps.id
  resource_group_name          = azurerm_resource_group.instance.name
  revision_mode                = "Single"

  template {
    container {
      name   = "onlinestore-website"
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
  name                         = "${var.name}-website"
  container_app_environment_id = azurerm_container_app_environment.apps.id
  resource_group_name          = azurerm_resource_group.instance.name
  revision_mode                = "Single"

  template {
    container {
      name   = "aspire-dashboard"
      image  = "onlinestorecontainerregistry.azurecr.io/dotnet/aspire-dashboard:8.0.0"
      cpu    = 0.25
      memory = "0.5Gi"
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
    password_secret_name = var.acr_password
  }
  
  lifecycle {
    ignore_changes = [
      template[0].container[0],
      registry,
      secret,
    ]
  }
}

# update the container app with extra additionalPortMappings, as this is not supported by the existing TF provider
resource "azapi_update_resource" "container_app_api" {
  type        = "Microsoft.App/containerApps@2023-11-02-preview"
  resource_id = azurerm_container_app.monitoring.id

  body = jsonencode({
    properties = {
      configuration = {
        ingress = {
          additionalPortMappings = [18889]
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
