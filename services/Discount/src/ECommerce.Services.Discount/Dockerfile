FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY services/Discount/src/ECommerce.Services.Discount/*.csproj Services/Discount/ECommerce.Services.Discount/
RUN dotnet restore Services/Discount/ECommerce.Services.Discount/*.csproj
COPY . .
RUN dotnet publish services/Discount/src/ECommerce.Services.Discount/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Services.Discount.dll" ]