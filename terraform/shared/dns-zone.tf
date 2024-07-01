resource "azurerm_dns_zone" "rockpal-co-uk" {
  name                = "rockpal.co.uk"
  resource_group_name = azurerm_resource_group.shared.name
}
