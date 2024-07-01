data "azurerm_dns_zone" "rockpal-co-uk"{
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

resource "azurerm_container_app_custom_domain" "api" {
  name                     = trimsuffix(trimprefix(azurerm_dns_txt_record.api.fqdn, "asuid."), ".")
  container_app_id         = azurerm_container_app.api.id

  depends_on = [
    azurerm_dns_txt_record.api,
  ]

  lifecycle {
    // When using an Azure created Managed Certificate these values must be added to ignore_changes to prevent resource recreation.
    ignore_changes = [certificate_binding_type, container_app_environment_certificate_id]
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

resource "azurerm_container_app_custom_domain" "website" {
  name                     = trimsuffix(trimprefix(azurerm_dns_txt_record.website.fqdn, "asuid."), ".")
  container_app_id         = azurerm_container_app.website.id

  depends_on = [
    azurerm_dns_txt_record.website,
  ]

  lifecycle {
    // When using an Azure created Managed Certificate these values must be added to ignore_changes to prevent resource recreation.
    ignore_changes = [certificate_binding_type, container_app_environment_certificate_id]
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

resource "azurerm_container_app_custom_domain" "monitoring" {
  name                     = trimsuffix(trimprefix(azurerm_dns_txt_record.monitoring.fqdn, "asuid."), ".")
  container_app_id         = azurerm_container_app.monitoring.id

  depends_on = [
    azurerm_dns_txt_record.monitoring,
  ]

  lifecycle {
    // When using an Azure created Managed Certificate these values must be added to ignore_changes to prevent resource recreation.
    ignore_changes = [certificate_binding_type, container_app_environment_certificate_id]
  }
}
