FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BC.APIGateway/BC.APIGateway.csproj", "BC.APIGateway/"]
RUN dotnet restore "BC.APIGateway/BC.APIGateway.csproj"
COPY . .
WORKDIR /src/BC.APIGateway
RUN dotnet build "BC.APIGateway.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "BC.APIGateway.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BC.APIGateway.dll"]