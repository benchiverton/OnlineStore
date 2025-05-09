variable "name" {
  type        = string
  description = "Name (/prefix) of the resource group"
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

variable "website_dns_subdomain" {
  type        = string
  description = "DNS subdomain for website"
}

variable "api_dns_subdomain" {
  type        = string
  description = "DNS subdomain for api"
}

variable "chat_dns_subdomain" {
  type        = string
  description = "DNS subdomain for chat server"
}

variable "monitoring_dns_subdomain" {
  type        = string
  description = "DNS subdomain for monitoring"
}
