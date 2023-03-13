#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DreamChip.AnimalTracking.WebApi/DreamChip.AnimalTracking.WebApi.csproj", "DreamChip.AnimalTracking.WebApi/"]
COPY ["DreamChip.AnimalTracking.Application/DreamChip.AnimalTracking.Application.csproj", "DreamChip.AnimalTracking.Application/"]
COPY ["DreamChip.AnimalTracking.Domain/DreamChip.AnimalTracking.Domain.csproj", "DreamChip.AnimalTracking.Domain/"]
COPY ["DreamChip.AnimalTracking.DAL/DreamChip.AnimalTracking.DAL.csproj", "DreamChip.AnimalTracking.DAL/"]
RUN dotnet restore "DreamChip.AnimalTracking.WebApi/DreamChip.AnimalTracking.WebApi.csproj"
COPY . .
WORKDIR "/src/DreamChip.AnimalTracking.WebApi"
RUN dotnet build "DreamChip.AnimalTracking.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DreamChip.AnimalTracking.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DreamChip.AnimalTracking.WebApi.dll"]