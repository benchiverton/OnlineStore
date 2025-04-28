#!/bin/sh

echo "`jq --arg APIBASEPATH "$API__BASEPATH" '.Api.BasePath=$APIBASEPATH' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
echo "`jq --arg APICLIENTID "$API__CLIENTID" '.Api.ClientId=$APICLIENTID' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
echo "`jq --arg WEBSITECLIENTID "$AZUREADB2C__CLIENTID" '.AzureAdB2C.ClientId=$WEBSITECLIENTID' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
echo "`jq --arg USEFAKEAUTH "$USEFAKEAUTH" '.UseFakeAuth=$USEFAKEAUTH' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
echo "`jq --arg CHATSERVERADDRESS "$CHATSERVER__ADDRESS" '.ChatServer.Address=$CHATSERVERADDRESS' /var/www/web/appsettings.json`" > /var/www/web/appsettings.json
