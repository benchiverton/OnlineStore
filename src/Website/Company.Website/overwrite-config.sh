cat /appsettings.json | jq --arg aVar "$(printenv API__BASEPATH)" '.Api.BasePath = $aVar' > /appsettings.json
