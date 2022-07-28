#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestBILite/TestBILite.csproj", "TestBILite/"]
RUN dotnet restore "TestBILite/TestBILite.csproj"
COPY . .
WORKDIR "/src/TestBILite"
RUN dotnet build "TestBILite.csproj" -c Release -o /app/build

FROM build AS Test
WORKDIR "/src/TestBILite"
CMD ["dotnet", "test"]