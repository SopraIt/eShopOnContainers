param
(
  [string] $servicePrincipalSecret
)

Write-Output "Requested By: $env:RELEASE_REQUESTEDFOREMAIL"

Write-Output "Log into Azure use cluster admin service principal"
az login --service-principal -u "$env:APP_SP_ID" -p "$servicePrincipalSecret" -t "$env:TENANT_ID"

Write-Output "Set the default Azure subscription"
az account set --subscription "$env:SUBSCRIPTION_NAME"

$SUBSCRIPTION_ID = $(az account show --query id -o tsv)
Write-Output "Subscription Id: $SUBSCRIPTION_ID"

$SUBSCRIPTION_NAME = $(az account show --query name -o tsv)
Write-Output "Subscription Name: $SUBSCRIPTION_NAME"

$TENANT_ID = $(az account show --query tenantId -o tsv)
Write-Output "Tenant Id: $TENANT_ID"

$USER_NAME = $(az account show --query user.name -o tsv)
Write-Output "Service Principal Name or ID: $USER_NAME"

# Relative path for kube config file
$KUBE_CONFIG_PATH = "./.kube/config"

Write-Output "Get credentials and set up for kubectl to use"
az aks get-credentials -g "$env:AKS_RG_NAME" -n "$env:AKS_CLUSTER_NAME" -a -f "$KUBE_CONFIG_PATH"

Write-Output "Get kubectl version info"
kubectl --kubeconfig "$KUBE_CONFIG_PATH" version short

Write-Output "Remove kubectl config file"
Remove-Item –path "$KUBE_CONFIG_PATH"