#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkerService2/WorkerService2.csproj", "WorkerService2/"]
RUN dotnet restore "./WorkerService2/./WorkerService2.csproj"
COPY . .
WORKDIR "/src/WorkerService2"
RUN dotnet build "./WorkerService2.csproj" -c $BUILD_CONFIGURATION -o /app/build

#ENV TZ="Asia/Jakarta"
#RUN apk add apt-get
#RUN apt-get install -yq tzdata
#RUN ln -sf /usr/share/zoneinfo/Asia/Jakarta /etc/localtime
#RUN dpkg-reconfigure -f moninteractive tzdata

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkerService2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService2.dll"]