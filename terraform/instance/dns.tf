data "azurerm_dns_zone" "rockpal-co-uk" {
  resource_group_name = "onlinestore-shared-rg"
  name                = "rockpal.co.uk"
}

resource "azurerm_dns_cname_record" "api" {
  name                = var.api_dns_subdomain
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record              = azurerm_container_app.api.ingress[0].fqdn
}

resource "azurerm_dns_txt_record" "api" {
  name                = "asuid.${var.api_dns_subdomain}"
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record {
    value = azurerm_container_app.api.custom_domain_verification_id
  }
}

resource "azurerm_dns_cname_record" "website" {
  name                = var.website_dns_subdomain
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record              = azurerm_container_app.website.ingress[0].fqdn
}

resource "azurerm_dns_txt_record" "website" {
  name                = "asuid.${var.website_dns_subdomain}"
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record {
    value = azurerm_container_app.website.custom_domain_verification_id
  }
}

resource "azurerm_dns_cname_record" "monitoring" {
  name                = var.monitoring_dns_subdomain
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record              = azurerm_container_app.monitoring.ingress[0].fqdn
}

resource "azurerm_dns_txt_record" "monitoring" {
  name                = "asuid.${var.monitoring_dns_subdomain}"
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record {
    value = azurerm_container_app.monitoring.custom_domain_verification_id
  }
}

module "container_apps_bind_dns" {
  source                                = "./container_apps_bind_dns"
  container_app_resource_group_name     = data.azurerm_container_app_environment.apps.resource_group_name
  container_app_env_name                = data.azurerm_container_app_environment.apps.name
  container_app_env_resource_group_name = data.azurerm_container_app_environment.apps.resource_group_name
  services = [
    {
      key                = "api",
      custom_domain      = trimsuffix(azurerm_dns_cname_record.api.fqdn, "."),
      container_app_name = azurerm_container_app.api.name
    },
    {
      key                = "website",
      custom_domain      = trimsuffix(azurerm_dns_cname_record.website.fqdn, "."),
      container_app_name = azurerm_container_app.website.name
    },
    {
      key                = "monitoring",
      custom_domain      = trimsuffix(azurerm_dns_cname_record.monitoring.fqdn, "."),
      container_app_name = azurerm_container_app.monitoring.name
    }
  ]

  depends_on = [ azapi_update_resource.monitoring_portmappings ]
}
