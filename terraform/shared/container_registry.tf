resource "azurerm_container_registry" "acr" {
  name                = "${var.name}-acr"
  resource_group_name = azurerm_resource_group.shared.name
  location            = azurerm_resource_group.shared.location
  sku                 = "Basic"
}
