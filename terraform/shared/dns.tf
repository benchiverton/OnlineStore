resource "azurerm_dns_a_record" "apps" {
  name                = azurerm_container_app_environment.apps.name
  zone_name           = azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  records             = [azurerm_container_app_environment.apps.static_ip_address]
}

resource "azurerm_dns_txt_record" "apps" {
  name                = "asuid.${azurerm_container_app_environment.apps.name}"
  zone_name           = azurerm_dns_zone.rockpal-co-uk.name
  resource_group_name = azurerm_dns_zone.rockpal-co-uk.resource_group_name
  ttl                 = 300
  record {
    value = azurerm_container_app_environment.apps.custom_domain_verification_id
  }
}
