terraform {}

resource "null_resource" "null" {
  for_each = { for svc in var.services : svc.key => svc }

  lifecycle {
    create_before_destroy = false
  }

  triggers = {
    ca_name        = each.value.container_app_name
    ca_rg_name     = var.container_app_resource_group_name
    ca_env_name    = var.container_app_env_name
    ca_env_rg_name = var.container_app_env_resource_group_name
    custom_domain  = each.value.custom_domain
  }

  # provision a managed cert and apply it to the container app
  provisioner "local-exec" {
    when    = create
    command = "sh ${path.module}/scripts/create.sh"

    environment = {
      CONTAINER_APP_NAME               = self.triggers.ca_name
      CONTAINER_APP_RESOURCE_GROUP     = self.triggers.ca_rg_name
      CONTAINER_APP_ENV_NAME           = self.triggers.ca_env_name
      CONTAINER_APP_ENV_RESOURCE_GROUP = self.triggers.ca_env_rg_name
      CUSTOM_DOMAIN                    = self.triggers.custom_domain
    }
  }

  provisioner "local-exec" {
    when    = destroy
    command = "sh ${path.module}/scripts/destroy.sh"

    environment = {
      CONTAINER_APP_NAME               = self.triggers.ca_name
      CONTAINER_APP_RESOURCE_GROUP     = self.triggers.ca_rg_name
      CONTAINER_APP_ENV_NAME           = self.triggers.ca_env_name
      CONTAINER_APP_ENV_RESOURCE_GROUP = self.triggers.ca_env_rg_name
      CUSTOM_DOMAIN                    = self.triggers.custom_domain
    }
  }
}