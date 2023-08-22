FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY Gateways/ECommerce.Gateway/*.csproj Gateways/ECommerce.Gateway/
RUN dotnet restore Gateways/ECommerce.Gateway/*.csproj
COPY . .
RUN dotnet publish Gateways/ECommerce.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Gateway.dll" ]