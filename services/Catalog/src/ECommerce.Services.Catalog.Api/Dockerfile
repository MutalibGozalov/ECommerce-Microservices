FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY services/Catalog/src/ECommerce.Services.Catalog.Domain/*.csproj Services/Catalog/src/ECommerce.Services.Catalog.Domain/
COPY services/Catalog/src/ECommerce.Services.Catalog.Application/*.csproj Services/Catalog/src/ECommerce.Services.Catalog.Application/
COPY services/Catalog/src/ECommerce.Services.Catalog.Infrastructure/*.csproj Services/Catalog/src/ECommerce.Services.Catalog.Infrastructure/
COPY services/Catalog/src/ECommerce.Services.Catalog.Api/*.csproj Services/Catalog/src/ECommerce.Services.Catalog.Api/
RUN dotnet restore Services/Catalog/src/ECommerce.Services.Catalog.Api/*.csproj
COPY . .
RUN dotnet publish services/Catalog/src/ECommerce.Services.Catalog.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Services.Catalog.Api.dll" ]