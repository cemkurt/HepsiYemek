#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HepsiYemek.APIs/HepsiYemek.APIs.csproj", "HepsiYemek.APIs/"]
COPY ["HepsiYemek.DataService/HepsiYemek.DataService.csproj", "HepsiYemek.DataService/"]
COPY ["HepsiYemek.Models/HepsiYemek.Models.csproj", "HepsiYemek.Models/"]
RUN dotnet restore "HepsiYemek.APIs/HepsiYemek.APIs.csproj"
COPY . .
WORKDIR "/src/HepsiYemek.APIs"
RUN dotnet build "HepsiYemek.APIs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HepsiYemek.APIs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HepsiYemek.APIs.dll"]
