$Env:ASPNETCORE_ENVIRONMENT = "Local-02"

Write-Host "Swagger MastersListClient"
npm run nswag-master-list

Write-Host "Swagger AuthenticationClient"
npm run nswag-authentication

Write-Host "Swagger BookingClient"
npm run nswag-booking

Write-Host "All done."
