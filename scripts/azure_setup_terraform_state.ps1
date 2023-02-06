# The below should be run in a powershell terminal.

$RESOURCE_GROUP_NAME = "onlinestoretfstate"
$STORAGE_ACCOUNT_NAME = "onlinestoretfstate"
$CONTAINER_NAME = "onlinestoretfstate-local" # update ENV to local/test/prod

# Create resource group
az group create --name $RESOURCE_GROUP_NAME --location eastus --subscription "Online Store"

# Create storage account
az storage account create --resource-group $RESOURCE_GROUP_NAME --name $STORAGE_ACCOUNT_NAME --sku Standard_LRS --encryption-services blob --subscription "Online Store"

# Get storage account key
$ACCOUNT_KEY=$(az storage account keys list --resource-group $RESOURCE_GROUP_NAME --account-name $STORAGE_ACCOUNT_NAME --subscription "Online Store" --query '[0].value' -o tsv)

# Create blob container
az storage container create --name $CONTAINER_NAME --account-name $STORAGE_ACCOUNT_NAME --account-key $ACCOUNT_KEY --subscription "Online Store"

Write-Output "storage_account_name: $STORAGE_ACCOUNT_NAME"
Write-Output "container_name: $CONTAINER_NAME"
Write-Output "access_key: $ACCOUNT_KEY"
