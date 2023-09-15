FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY services/Order/src/ECommerce.Services.Order.Domain/*.csproj Services/Order/src/ECommerce.Services.Order.Domain/
COPY services/Order/src/ECommerce.Services.Order.Application/*.csproj Services/Order/src/ECommerce.Services.Order.Application/
COPY services/Order/src/ECommerce.Services.Order.Infrastructure/*.csproj Services/Order/src/ECommerce.Services.Order.Infrastructure/
COPY services/Order/src/ECommerce.Services.Order.Api/*.csproj Services/Order/src/ECommerce.Services.Order.Api/
RUN dotnet restore Services/Order/src/ECommerce.Services.Order.Api/*.csproj
COPY . .
RUN dotnet publish services/Order/src/ECommerce.Services.Order.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Services.Order.Api.dll" ]