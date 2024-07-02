#!/bin/bash

# functions below taken from: https://stackoverflow.com/a/25515370
yell() { echo "$0: $*" >&2; }
die() {
  yell "$*"
  exit 111
}

# get the managed cert using the custom domain
CERTIFICATE_ID=$(
  az containerapp env certificate list \
    -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
    -n $CONTAINER_APP_ENV_NAME \
    --managed-certificates-only \
    --query "[?properties.subjectName=='$CUSTOM_DOMAIN'].id" \
    --output tsv
)

# destroy the cert
az containerapp env certificate delete \
  -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
  -n $CONTAINER_APP_ENV_NAME \
  --certificate $CERTIFICATE_ID --yes
echo "destroyed the managed certificate"

# remove the custom domain from the container app
az containerapp hostname delete --hostname $CUSTOM_DOMAIN \
  -g $CONTAINER_APP_RESOURCE_GROUP \
  -n $CONTAINER_APP_NAME
echo "removed the custom domain from the container app"
