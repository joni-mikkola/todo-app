param prefix string
param adminLogin string
param location string

@secure()
param adminPassword string

resource sqlServer 'Microsoft.Sql/servers@2014-04-01' = {
  name: '${prefix}-sqlserver'
  location: location
  properties: {
    administratorLogin: adminLogin
    administratorLoginPassword: adminPassword
  }
}

resource sqlServerDatabase 'Microsoft.Sql/servers/databases@2014-04-01' = {
  parent: sqlServer
  name: '${prefix}database'
  location: location
  properties: {
    edition: 'Basic'
  }
}

resource SQLAllowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2020-11-01-preview' = {
  name: 'AllowAllWindowsAzureIps'
  parent: sqlServer
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

output connectionString string = 'Server=tcp:${reference(sqlServer.id).fullyQualifiedDomainName},1433;Initial Catalog=${sqlServerDatabase.name};User Id=${adminLogin};Password=${adminPassword}'
