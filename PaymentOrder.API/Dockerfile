#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PaymentOrder.API/PaymentOrder.API.csproj", "PaymentOrder.API/"]
RUN dotnet restore "PaymentOrder.API/PaymentOrder.API.csproj"
COPY . .
WORKDIR "/src/PaymentOrder.API"
RUN dotnet build "PaymentOrder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentOrder.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentOrder.API.dll"]