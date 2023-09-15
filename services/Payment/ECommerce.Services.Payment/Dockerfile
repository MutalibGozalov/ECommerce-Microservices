FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY services/Payment/ECommerce.Services.Payment/*.csproj Services/Payment/ECommerce.Services.Payment/
RUN dotnet restore Services/Payment/ECommerce.Services.Payment/*.csproj
COPY . .
RUN dotnet publish services/Payment/ECommerce.Services.Payment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Services.Payment.dll" ]