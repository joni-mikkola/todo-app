# Vue 3 + .NET 6 - Todo app

### Libraries

    - Axios
    - Moment
    - Vuex
    - Vue-datepicker
    - Vue-toastification

    - Bicep for infrastructure

## Deployment to azure

Source code includes bicep configuration to deploy necessary infrastructure to Azure and then deploy the app. You need to have resource group set up beforehand. Components deployed:

- App service
- App service plan
- SQL Server
- SQL database

Run script deploy.sh with parameters and provide sql admin password:

`sh deploy.sh IAC/main.bicep your_resource_group_name location `

## Running locally

- cd MyApp
- dotnet run &
- cd ClientApp
- npm install
- npm run serve
- browse to https://localhost:8080
