cat /var/www/web/appsettings.json | jq --arg aVar "$(printenv API__BASEPATH)" '.Api.BasePath = $aVar' > /var/www/web/appsettings.json
