FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BC.API/BC.API.csproj", "BC.API/"]
RUN dotnet restore "BC.API/BC.API.csproj"
COPY . .
WORKDIR /src/BC.API
RUN dotnet build "BC.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "BC.API.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BC.API.dll"]
