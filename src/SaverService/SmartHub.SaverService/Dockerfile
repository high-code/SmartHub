FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/SaverService/SmartHub.SaverService/SmartHub.SaverService.csproj src/SaverService/SmartHub.SaverService/
RUN dotnet restore src/SaverService/SmartHub.SaverService/SmartHub.SaverService.csproj
COPY . .
WORKDIR /src/src/SaverService/SmartHub.SaverService
RUN dotnet build SmartHub.SaverService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SmartHub.SaverService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.SaverService.dll"]
