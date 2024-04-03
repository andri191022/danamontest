#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
#EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Development

#
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Testing.BillPay.csproj", "Testing.BillPay/"]
RUN dotnet restore "./Testing.BillPay/./Testing.BillPay.csproj"

WORKDIR "/src/Testing.BillPay"
COPY . .

RUN dotnet build "./Testing.BillPay.csproj" -c BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Testing.BillPay.csproj" -c BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testing.BillPay.dll"]

##FROM mcr.microsoft.com/dotnet/sdk:latest AS build
##WORKDIR /App
##
### Copy everything
##COPY . ./
### Restore as distinct layers
##RUN dotnet restore
### Build and publish a release
##RUN dotnet publish -c Release -o out
##
### Build runtime image
##FROM mcr.microsoft.com/dotnet/aspnet:latest
##WORKDIR /App
##COPY --from=build /App/out .
##ENTRYPOINT ["dotnet", "Testing.BillPay.dll"]