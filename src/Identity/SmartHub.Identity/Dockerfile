FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Identity/SmartHub.Identity/SmartHub.Identity.csproj", "src/Identity/SmartHub.Identity/"]
RUN dotnet restore "src/Identity/SmartHub.Identity/SmartHub.Identity.csproj"
COPY . .
WORKDIR "/src/src/Identity/SmartHub.Identity"
RUN dotnet build "SmartHub.Identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHub.Identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.Identity.dll"]