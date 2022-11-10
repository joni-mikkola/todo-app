param prefix string
param connectionString string
param location string

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: '${prefix}-plan'
  location: location
  sku: {
    name: 'B1'
  }
  kind: 'windows'
}

resource webApplication 'Microsoft.Web/sites@2021-01-15' = {
  name: '${prefix}-app'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      alwaysOn: true
      netFrameworkVersion: 'v6.0'
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: connectionString
          type: 'SQLServer'
        }
      ]
    }
  }
}
