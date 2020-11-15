$Env:ASPNETCORE_ENVIRONMENT = "Local-02"

dotnet ef migrations add --context FeedbackContext -o Services/FeedbackService/Data/Migrations Init

Write-Host "Done!"