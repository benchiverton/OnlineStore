resource "azurerm_dns_zone" "englishwinetourism-co-uk" {
  name                = "englishwinetourism.co.uk"
  resource_group_name = azurerm_resource_group.shared.name
}
