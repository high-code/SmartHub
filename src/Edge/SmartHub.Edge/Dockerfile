FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Edge/SmartHub.Edge/SmartHub.Edge.csproj", "src/Edge/SmartHub.Edge/"]
RUN dotnet restore "src/Edge/SmartHub.Edge/SmartHub.Edge.csproj"
COPY . .
WORKDIR "/src/src/Edge/SmartHub.Edge"
RUN dotnet build "SmartHub.Edge.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHub.Edge.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.Edge.dll"]