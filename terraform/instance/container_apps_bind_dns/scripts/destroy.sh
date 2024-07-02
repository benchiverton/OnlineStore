#!/bin/bash

# functions below taken from: https://stackoverflow.com/a/25515370
yell() { echo "$0: $*" >&2; }
die() {
  yell "$*"
  exit 111
}

# remove the custom domain from the container app
az containerapp hostname delete --hostname $CUSTOM_DOMAIN \
  -g $CONTAINER_APP_RESOURCE_GROUP \
  -n $CONTAINER_APP_NAME
echo "removed the custom domain from the container app"

# wait for the custom domain to be removed
tries=0
until [ "$tries" -ge 12 ]; do
  [[ -z $(
    az containerapp hostname list \
      -n $CONTAINER_APP_NAME \
      -g $CONTAINER_APP_RESOURCE_GROUP \
      --query "[?name=='$CUSTOM_DOMAIN'].name" \
      --output tsv
  ) ]] && break
  tries=$((tries + 1))

  sleep 10
done
if [ "$tries" -ge 12 ]; then
  die "waited for 2 minutes, checked the containerapp 12 times and it still has the custom domain. check azure portal..."
fi

# get the managed cert using the custom domain
CERTIFICATE_ID=$(
  az containerapp env certificate list \
    -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
    -n $CONTAINER_APP_ENV_NAME \
    --managed-certificates-only \
    --query "[?properties.subjectName=='$CUSTOM_DOMAIN'].id" \
    --output tsv
)

# remove the custom domain from the container app
az containerapp hostname delete --hostname $CUSTOM_DOMAIN \
  -g $CONTAINER_APP_RESOURCE_GROUP \
  -n $CONTAINER_APP_NAME
echo "removed the custom domain from the container app"

# wait for the custom domain to be removed
tries=0
until [ "$tries" -ge 12 ]; do
  [[ -z $(
    az containerapp hostname list \
      -n $CONTAINER_APP_NAME \
      -g $CONTAINER_APP_RESOURCE_GROUP \
      --query "[?name=='$CUSTOM_DOMAIN'].name" \
      --output tsv
  ) ]] && break
  tries=$((tries + 1))

  sleep 10
done
if [ "$tries" -ge 12 ]; then
  die "waited for 2 minutes, checked the containerapp 12 times and it still has the custom domain. check azure portal..."
fi

# destroy the cert
az containerapp env certificate delete \
  -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
  -n $CONTAINER_APP_ENV_NAME \
  --certificate $CERTIFICATE_ID --yes
echo "destroyed the managed certificate"
