FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY services/Cart/src/ECommerce.Services.Cart.Domain/*.csproj Services/Cart/src/ECommerce.Services.Cart.Domain/
COPY services/Cart/src/ECommerce.Services.Cart.Application/*.csproj Services/Cart/src/ECommerce.Services.Cart.Application/
COPY services/Cart/src/ECommerce.Services.Cart.Infrastructure/*.csproj Services/Cart/src/ECommerce.Services.Cart.Infrastructure/
COPY services/Cart/src/ECommerce.Services.Cart.Api/*.csproj Services/Cart/src/ECommerce.Services.Cart.Api/
RUN dotnet restore Services/Cart/src/ECommerce.Services.Cart.Api/*.csproj
COPY . .
RUN dotnet publish services/Cart/src/ECommerce.Services.Cart.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Services.Cart.Api.dll" ]