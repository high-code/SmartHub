FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
RUN npm install -g @angular/cli	

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/Spa/SmartHub.SPA/SmartHub.SPA.csproj", "src/Spa/SmartHub.SPA/"]
RUN dotnet restore "src/Spa/SmartHub.SPA/SmartHub.SPA.csproj"
COPY . .
WORKDIR "/src/src/Spa/SmartHub.SPA"
RUN dotnet build "SmartHub.SPA.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHub.SPA.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHub.SPA.dll"]