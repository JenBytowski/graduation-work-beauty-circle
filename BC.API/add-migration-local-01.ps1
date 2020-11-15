$Env:ASPNETCORE_ENVIRONMENT = "Local-01"

dotnet ef migrations add --context FeedbackContext -o Services/FeedbackService/Data/Migrations AddServiceDataToBookingFeedback

Write-Host "Done!"