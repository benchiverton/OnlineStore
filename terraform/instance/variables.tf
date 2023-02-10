variable "name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "onlinestore"
}

variable "environment" {
  type        = string
  description = "Name of the deployment environment"
  default     = "local"
}

variable "location" {
  type        = string
  description = "Location to deploy the resoruce group"
  default     = "East US 2"
}

variable "dns_prefix" {
  type        = string
  description = "A prefix for any dns based resources"
  default     = "os"
}

variable "plan_sku" {
  type        = string
  description = "The sku of app service plan to create"
  default     = "F1"
}

variable "acr_username" {
  type        = string
  description = "The username to log in to ACR"
  sensitive   = true
}

variable "acr_password" {
  type        = string
  description = "The password to log in to ACR"
  sensitive   = true
}
