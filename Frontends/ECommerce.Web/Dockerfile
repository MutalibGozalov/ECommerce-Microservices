FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
COPY Shared/ECommerce.Shared/*.csproj Shared/ECommerce.Shared/
COPY Frontends/ECommerce.Web/*.csproj Frontends/ECommerce.Web/
RUN dotnet restore Frontends/ECommerce.Web/*.csproj
COPY . .
RUN dotnet publish Frontends/ECommerce.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ECommerce.Web.dll" ]