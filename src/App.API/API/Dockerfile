#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY "src/App.API/App.Api.sln" "./App.API/"
COPY "src/App.API/API/*.csproj" "App.API/API/"
COPY "src/App.API/API.DataAccess/App.API.DataAccess.csproj" "App.API/API.DataAccess/"
COPY "src/App.API/API.Infrastructure/App.API.Infrastructure.csproj" "App.API/API.Infrastructure/"
COPY "src/App.API/API.Services/App.API.Services.csproj" "App.API/API.Services/"
COPY "src/App.API/Tests/API.Tests/*.csproj" "App.API/Tests/API.Tests/"
RUN dotnet restore "App.API/App.Api.sln"
COPY ./src .
WORKDIR "/src/App.API"
RUN dotnet build "API/App.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API/App.API.csproj" -c Release -o /app/publish

FROM build AS dev
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
# this env variable avoid default ports (http:5000 and https:5001), which fail to open in the docker image
# WARNING: when we activate https, port 80 stops working (I don't know why). You can enter swagger UI but
# all the controller actions fail to execute.
# ENV ASPNETCORE_URLS=https://+:443;http://+:80
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app/build
ENTRYPOINT ["dotnet", "App.API.dll"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.API.dll"]