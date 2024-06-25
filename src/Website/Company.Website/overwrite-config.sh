echo "running overwrite-config!"
cat /var/www/web/appsettings.json
echo "`jq --arg APIBASEPATH "$API__BASEPATH" '.Api.BasePath=$APIBASEPATH' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
