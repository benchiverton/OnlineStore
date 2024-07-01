variable "container_app_resource_group_name" {
  description = "name of the resource group where the container apps are deployed"
  type = string
}

variable "container_app_env_name" {
  description = "name of the container app environment name"
  type = string
}

variable "container_app_env_resource_group_name" {
  description = "name of the resource group where the container app environment is deployed"
  type = string
}

variable "services" {
  type = list(object({
    key = string
    custom_domain = string
    container_app_name = string
  }))
}