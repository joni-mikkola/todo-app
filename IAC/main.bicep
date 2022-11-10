param location string
param name string

@secure()
param sqlAdminPassword string

module sql './sql.bicep' = {
  name: 'deploySQL'
  params: {
    prefix: name
    location: location
    adminLogin: name
    adminPassword: sqlAdminPassword
  }
}

module webapp './webapp.bicep' = {
  name: 'deployWebApp'
  params: {
    prefix: name
    location: location
    connectionString: sql.outputs.connectionString
  }
}
