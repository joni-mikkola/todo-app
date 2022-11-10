#!/bin/bash
templateFile=$1
name=$2
location=$3

az deployment group create --name tododeployment --resource-group $name --template-file $templateFile --parameters location=$location name=$name
dotnet publish -o publish
cd publish && zip -r ../publish.zip . && cd ..
az webapp deployment source config-zip --src publish.zip -n $name-app -g $name
rm -fr publish publish.zip
