Param(
    [parameter(Mandatory=$false)][string]$registry,
    [parameter(Mandatory=$false)][string]$dockerUser,
    [parameter(Mandatory=$false)][string]$dockerPassword,
    [parameter(Mandatory=$false)][string]$externalDns,
    [parameter(Mandatory=$false)][string]$appName="eshop",
    [parameter(Mandatory=$false)][bool]$deployInfrastructure=$true,
    [parameter(Mandatory=$false)][bool]$deployCharts=$true,
    [parameter(Mandatory=$false)][bool]$clean=$true,
    [parameter(Mandatory=$false)][string]$aksName="",
    [parameter(Mandatory=$false)][string]$aksRg="",
    [parameter(Mandatory=$false)][string]$imageTag="latest",
    [parameter(Mandatory=$false)][bool]$useLocalk8s=$false,
    [parameter(Mandatory=$false)][bool]$useCustomRegistry=$false
    ) 

$dns = $externalDns

$ingressValuesFile="ingress_values.yaml"

if ($useLocalk8s -eq $true) {
    $ingressValuesFile="ingress_values_dockerk8s.yaml"
    $dns="localhost"
}

if ($externalDns -eq "aks") {
    if  ([string]::IsNullOrEmpty($aksName) -or [string]::IsNullOrEmpty($aksRg)) {
        Write-Host "Error: When using -dns aks, MUST set -aksName and -aksRg too." -ForegroundColor Red
        exit 1
    }
    Write-Host "Getting DNS of AKS of AKS $aksName (in resource group $aksRg)..." -ForegroundColor Green
    $dns = $(az aks show -n $aksName  -g $aksRg --query addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName)
    if ([string]::IsNullOrEmpty($dns)) {
        Write-Host "Error getting DNS of AKS $aksName (in resource group $aksRg). Please ensure AKS has httpRouting enabled AND Azure CLI is logged & in version 2.0.37 or higher" -ForegroundColor Red
        exit 1
    }
    $dns = $dns -replace '[\"]'
    Write-Host "DNS base found is $dns. Will use $appName.$dns for the app!" -ForegroundColor Green
    $dns = "$appName.$dns"
}

# Initialization & check commands
if ([string]::IsNullOrEmpty($dns)) {
    Write-Host "No DNS specified. Ingress resources will be bound to public ip" -ForegroundColor Yellow
}

#helm init --upgrade

if ($clean) {
    Write-Host "Cleaning previous helm releases..." -ForegroundColor Green
    helm delete --purge $(helm ls -q) 
    kubectl delete pvc -l component=data
    Write-Host "Previous releases deleted" -ForegroundColor Green
}



if (-not [string]::IsNullOrEmpty($registry)) {
    $useCustomRegistry=$true
    if ([string]::IsNullOrEmpty($dockerUser) -or [string]::IsNullOrEmpty($dockerPassword)) {
        Write-Host "Error: Must use -dockerUser AND -dockerPassword if specifying custom registry" -ForegroundColor Red
        exit 1
    }
}

Write-Host "Begin eShopOnContainers installation using Helm" -ForegroundColor Green

Write-Host "helm repo update" -ForegroundColor Green
helm init --client-only
helm repo update
helm dependency update elastic-stack

#$infras = ("sql-data", "nosql-data", "rabbitmq", "keystore-data", "basket-data","elasticsearch","kibana","logstash")
#$charts = ("eshop-common", "apigwmm", "apigwms", "apigwwm", "apigwws", "basket-api","catalog-api", "catalog-backgroundtasks", "catalog-fullimport-job","identity-api", "locations-api", "marketing-api", "mobileshoppingagg","ordering-api","ordering-backgroundtasks","ordering-signalrhub", "payment-api", "webmvc", "webshoppingagg", "webspa", "webstatus", "webhooks-api", "webhooks-web")
$infras = ("sql-data", "nosql-data", "rabbitmq", "keystore-data", "basket-data","elasticsearch","kibana","logstash")
$charts = ("eshop-common", "apigwws", "basket-api","catalog-api", "catalog-fullimport-job","catalog-backgroundtasks","identity-api", "locations-api", "marketing-api","ordering-api","ordering-backgroundtasks","ordering-signalrhub", "payment-api", "webspa", "webstatus", "webhooks-api", "webhooks-web")


if ($deployInfrastructure) {
    foreach ($infra in $infras) {
        Write-Host "Installing infrastructure: $infra" -ForegroundColor Green
        helm install --values app.yaml --values inf.yaml --values $ingressValuesFile --set app.name=$appName --set inf.k8s.dns=$dns --set ingress.hosts="{$dns}" --name="$appName-$infra" $infra     
    }
}
else {
    Write-Host "eShopOnContainers infrastructure (bbdd, redis, ...) charts aren't installed (-deployCharts is false)" -ForegroundColor Yellow
}

if ($deployCharts) {
    foreach ($chart in $charts) {
        Write-Host "Installing: $chart" -ForegroundColor Green
        if ($useCustomRegistry) {
            Write-Host "Installing1: $chart"
            helm install --set inf.registry.server=$registry --set inf.registry.login=$dockerUser --set inf.registry.pwd=$dockerPassword --set inf.registry.secretName=eshop-docker-scret --values app.yaml --values inf.yaml --values $ingressValuesFile --set app.name=$appName --set inf.k8s.dns=$dns --set ingress.hosts="{$dns}" --set image.tag=$imageTag --set image.pullPolicy=Always --name="$appName-$chart" $chart 
        }
        else {
            if ($chart -ne "eshop-common")  {       # eshop-common is ignored when no secret must be deployed
                Write-Host "Installing2: $chart"
                helm install --values app.yaml --values inf.yaml --values $ingressValuesFile --set app.name=$appName --set inf.k8s.dns=$dns  --set ingress.hosts="{$dns}" --set image.tag=$imageTag --set image.pullPolicy=Always --name="$appName-$chart" $chart 
            }
        }
    }
}
else {
    Write-Host "eShopOnContainers non-infrastructure charts aren't installed (-deployCharts is false)" -ForegroundColor Yellow
}



# Write-Host "Installing: ELK Stack" -ForegroundColor Green
# helm install --name elk-stack stable/elastic-stack --version 1.6.0

Write-Host "helm charts installed." -ForegroundColor Green





