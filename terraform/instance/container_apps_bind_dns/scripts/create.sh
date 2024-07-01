# env variables used throughout this script:
# CONTAINER_APP_NAME
# CONTAINER_APP_RESOURCE_GROUP
# CONTAINER_APP_ENV_NAME
# CONTAINER_APP_ENV_RESOURCE_GROUP
# CUSTOM_DOMAIN


# functions below taken from: https://stackoverflow.com/a/25515370
yell() { echo "$0: $*" >&2; }
die() {
  yell "$*"
  exit 111
}

# use dig to verify the asuid txt record exists on the DNS host
# azure requires this to exist prior to adding the domain
# azure's dns can also be slow, so best to check propagation
tries=0
until [ "$tries" -ge 12 ]; do
  [[ ! -z $(dig @8.8.8.8 txt asuid.$CUSTOM_DOMAIN +short) ]] && break
  tries=$((tries + 1))
  sleep 10
done
if [ "$tries" -ge 12 ]; then
  die "'asuid.${CUSTOM_DOMAIN}' txt record does not exist"
fi

echo "took $tries trie(s) for the dns record to exist publically"

# check if the hostname already exists on the container app
# if not, add it since it's required to provision a managed cert
DOES_CUSTOM_DOMAIN_EXIST=$(
  az containerapp hostname list \
    -n $CONTAINER_APP_NAME \
    -g $CONTAINER_APP_RESOURCE_GROUP \
    --query "[?name=='$CUSTOM_DOMAIN'].name" \
    --output tsv
)
if [ -z "${DOES_CUSTOM_DOMAIN_EXIST}" ]; then
  echo "adding custom hostname to container app first since it does not exist yet"
  az containerapp hostname add \
    -n $CONTAINER_APP_CONTAINER_APP_NAME \
    -g $CONTAINER_APP_RESOURCE_GROUP \
    --hostname $CUSTOM_DOMAIN \
    --output none
fi

# check if a managed cert for the domain already exists
# if it does not exist, provision one
# if it does, save its name to use for binding it later
MANAGED_CERTIFICATE_NAME=$(
  az containerapp env certificate list \
    -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
    -n $CONTAINER_APP_ENV_NAME \
    --managed-certificates-only \
    --query "[?properties.subjectName=='$CUSTOM_DOMAIN'].name" \
    --output tsv
)
if [ -z "${MANAGED_CERTIFICATE_NAME}" ]; then
  MANAGED_CERTIFICATE_NAME=$(
    az containerapp env certificate create \
      -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
      -n $CONTAINER_APP_ENV_NAME \
      --hostname $CUSTOM_DOMAIN \
      --validation-method CNAME \
      --query "name" \
      --output tsv
  )
  echo "created cert for '$CUSTOM_DOMAIN'. waiting for it to provision now..."

  # poll azcli to check for the certificate status
  # this is better than waiting 5 minutes, because it could be
  # faster and we get to exit the script faster
  # ---
  # the default 20 tries means it'll check for 5 mins
  # at 15 second intervals
  tries=0
  until [ "$tries" -ge 20 ]; do
    STATE=$(
      az containerapp env certificate list \
        -g $CONTAINER_APP_ENV_RESOURCE_GROUP \
        -n $CONTAINER_APP_ENV_NAME \
        --managed-certificates-only \
        --query "[?properties.subjectName=='$CUSTOM_DOMAIN'].properties.provisioningState" \
        --output tsv
    )
    [[ $STATE == "Succeeded" ]] && break
    tries=$((tries + 1))

    sleep 15
  done
  if [ "$tries" -ge 20 ]; then
    die "waited for 5 minutes, checked the certificate status 20 times and its not done. check azure portal..."
  fi
else
  echo "found existing cert in the env. proceeding to use that"
fi

# check if the cert has already been bound
# if not, bind it then
IS_CERT_ALREADY_BOUND=$(
  az containerapp hostname list \
    -n $CONTAINER_APP_NAME \
    -g $CONTAINER_APP_RESOURCE_GROUP \
    --query "[?name=='$CUSTOM_DOMAIN'].bindingType" \
    --output tsv
)
if [ $IS_CERT_ALREADY_BOUND = "SniEnabled" ]; then
  echo "cert is already bound, exiting..."
else
  # try bind the cert to the container app
  echo "cert successfully provisioned. binding the cert id to the hostname"
  az containerapp hostname bind \
    -g $CONTAINER_APP_RESOURCE_GROUP \
    -n $CONTAINER_APP_NAME \
    --hostname $CUSTOM_DOMAIN \
    --environment $CONTAINER_APP_ENV_NAME \
    --certificate $MANAGED_CERTIFICATE_NAME \
    --output none
  echo "finished binding. the domain is now secured and ready to use"
fi