data "azurerm_dns_zone" "rockpal-co-uk" {
  name                = "rockpal.co.uk"
  resource_group_name = "onlinestore-shared-rg"
}

resource "azurerm_dns_cname_record" "website" {
  name                = var.website_dns_subdomain
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record              = "${var.name}-website.${azurerm_container_app_environment.apps.default_domain}" # here
}

resource "azurerm_dns_txt_record" "website" {
  name                = "asuid.${azurerm_dns_cname_record.website.name}"
  zone_name           = data.azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = data.azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record {
    value = azurerm_container_app_environment.apps.custom_domain_verification_id
  }
}

# resource "azurerm_app_service_custom_hostname_binding" "website" {
#   hostname            = trim(azurerm_dns_cname_record.website.fqdn, ".")
#   app_service_name    = azurerm_container_app.website.name
#   resource_group_name = azurerm_resource_group.instance.name
#   depends_on          = [azurerm_dns_txt_record.website]

#   # Ignore ssl_state and thumbprint as they are managed using
#   # azurerm_app_service_certificate_binding.website
#   lifecycle {
#     ignore_changes = [ssl_state, thumbprint]
#   }
# }

# resource "azurerm_app_service_managed_certificate" "website" {
#   custom_hostname_binding_id = azurerm_app_service_custom_hostname_binding.website.id
# }

# resource "azurerm_app_service_certificate_binding" "website" {
#   hostname_binding_id = azurerm_app_service_custom_hostname_binding.website.id
#   certificate_id      = azurerm_app_service_managed_certificate.website.id
#   ssl_state           = "SniEnabled"
# }
