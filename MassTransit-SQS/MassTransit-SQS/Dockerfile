FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=debug
WORKDIR /src
COPY ["MassTransit-SQS/MassTransit-SQS.csproj", "MassTransit-SQS/"]
RUN dotnet restore "MassTransit-SQS/MassTransit-SQS.csproj"
COPY . .
WORKDIR "/src/MassTransit-SQS"
RUN dotnet build "MassTransit-SQS.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MassTransit-SQS.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit-SQS.dll"]
