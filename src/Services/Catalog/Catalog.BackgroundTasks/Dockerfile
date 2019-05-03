# FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
# WORKDIR /src
# COPY ["Catalog.BackgroundTasks/Catalog.BackgroundTasks.csproj", "Catalog.BackgroundTasks/"]
# RUN dotnet restore "Catalog.BackgroundTasks/Catalog.BackgroundTasks.csproj"
# COPY . .
# WORKDIR "/src/Catalog.BackgroundTasks"
# RUN dotnet build "Catalog.BackgroundTasks.csproj" -c Release -o /app

# FROM build AS publish
# RUN dotnet publish "Catalog.BackgroundTasks.csproj" -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "Catalog.BackgroundTasks.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY . .
WORKDIR /src/src/Services/Catalog/Catalog.BackgroundTasks
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore  -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Catalog.BackgroundTasks.dll"]