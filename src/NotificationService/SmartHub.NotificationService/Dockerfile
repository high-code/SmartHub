FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/NotificationService/SmartHub.NotificationService/SmartHub.NotificationService.csproj", "src/NotificationService/SmartHub.NotificationService/"]
RUN dotnet restore "src/NotificationService/SmartHub.NotificationService/SmartHub.NotificationService.csproj"
COPY . .
WORKDIR "/src/src/NotificationService/SmartHub.NotificationService"
RUN dotnet build "SmartHub.NotificationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHub.NotificationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.NotificationService.dll"]