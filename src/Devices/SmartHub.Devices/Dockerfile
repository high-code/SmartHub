FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Api/SmartHub.Api/SmartHub.Api.csproj", "src/Api/SmartHub.Api/"]
RUN dotnet restore "src/Api/SmartHub.Api/SmartHub.Api.csproj"
COPY . .
WORKDIR "/src/src/Api/SmartHub.Api"
RUN dotnet build "SmartHub.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHub.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.Api.dll"]