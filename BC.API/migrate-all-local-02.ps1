$Env:ASPNETCORE_ENVIRONMENT = "Local-02"

Write-Host "Migrating AuthenticationContext"
dotnet ef database update --context AuthenticationContext

Write-Host "Migrating BalanceContext"
dotnet ef database update --context BalanceContext

Write-Host "Migrating BookingContext"
dotnet ef database update --context BookingContext

Write-Host "Migrating MastersContext"
dotnet ef database update --context MastersContext

Write-Host "Migrating FeedbackContext"
dotnet ef database update --context FeedbackContext

Write-Host "Done!"