cat /var/www/web/appsettings.json | jq --arg aVar "$(printenv API__BASEPATH)" '.API.BASEPATH = $aVar' > /var/www/web/appsettings.json
