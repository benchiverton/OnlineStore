resource "azurerm_aadb2c_directory" "rockpal" {
  country_code            = "GB"
  data_residency_location = "Europe"
  display_name            = "rockpal-b2c-tenant"
  domain_name             = "rockpal.co.uk"
  resource_group_name     = azurerm_resource_group.permanent.name
  sku_name                = "PremiumP1"
}
