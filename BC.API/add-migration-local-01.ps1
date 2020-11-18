$Env:ASPNETCORE_ENVIRONMENT = "Local-01"

dotnet ef migrations add --context BookingContext -o Services/BookingService/Data/Migrations Change_Schedule_Schema

Write-Host "Done!"