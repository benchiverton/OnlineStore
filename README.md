# Online Store

Blazor WebAssembly website backed by a RESTful API for a generic online store.

## Deployment
![Deploy - prod](https://github.com/benchiverton/OnlineStore/actions/workflows/deploy-prod.yml/badge.svg)

This app is deployed via GitHub Actions using Terraform into Azure.

### Local terraform environment
1. Install terraform CLI (https://learn.hashicorp.com/tutorials/terraform/install-cli).
2. Install Azure CLI (https://learn.hashicorp.com/tutorials/terraform/azure-build).
3. (azure portal) Create a subscription called 'Online Store'.
4. (powershell) Authenticate with Azure (`az login`).
5. (powershell) Run the contents of [azure_setup_terraform_state.ps1](scripts/azure_setup_terraform_state.ps1) for each environment (local/test/prod), updating the value of `$CONTAINER_NAME` each time.
6. (powershell) Initialise terraform from the [terraform](terraform) directory (`terraform init -backend-config="backend.local.hcl"`).

### GitHub runner

The GitHub runner needs to connect to Azure Storage (Terraform state file) and other Azure services (app deployment) to execute this repositories Actions. Sensitive connection properties are stored in the repository secrets.

#### Creating the service principal

1. (powershell) Authenticate with Azure (`az login`).
2. (powershell) Get your subscription id (`az account list`).
3. (powershell) Specify which subscription you want to use (`az account set --subscription="SUBSCRIPTION_ID"`).
4. (powershell) Create the service principal in the 'Online Store' subscription (`az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/SUBSCRIPTION_ID" --sdk-auth`). This will output some JSON that the runner will use to log into Azure (stored in repo secret `AZURE_CREDENTIALS`). In addition, the following repository secrets should be extracted from this JSON (which are needed for terraform steps):
   * *appId* - `TF_VAR_AGENT_CLIENT_ID`
   * *password* - `TF_VAR_AGENT_CLIENT_SECRET`
   * *tenant* - `TF_VAR_TENANT_ID`

For more information, read ['Azure Provider: Authenticating using a Service Principal with a Client Secret'](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/guides/service_principal_client_secret).

#### Repository secrets
aa
`az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/SUBSCRIPTION_ID"`

| Name                         | Description                                               | Example value                                 |
| ---------------------------- | --------------------------------------------------------- | --------------------------------------------- |
| `AZURE_CREDENTIALS`          | The password of the service principal account.            | `{ FULL JSON FROM SERVICE PRINCIPAL CREATE }` |
| `TF_VAR_AGENT_CLIENT_ID`     | The id of the service principal account.                  | `00000000-0000-0000-0000-000000000000`        |
| `TF_VAR_AGENT_CLIENT_SECRET` | The password of the service principal account.            | `XXXXXXXXXXXXXXXXXXXXXXXXXXX-X-XXXX`          |
| `TF_VAR_SUBSCRIPTION_ID`     | The id of the 'Online Store' subscription.                | `00000000-0000-0000-0000-000000000000`        |
| `TF_VAR_TENANT_ID`           | The tenant that the service principal account belongs to. | `00000000-0000-0000-0000-000000000000`        |
