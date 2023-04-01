variable "name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "onlinestore"
}

variable "location" {
  type        = string
  description = "Location to deploy the resoruce group"
  default     = "East US 2"
}
