#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebAppPatientCare/WebAppPatientCare.csproj", "WebAppPatientCare/"]
RUN dotnet restore "WebAppPatientCare/WebAppPatientCare.csproj"
COPY . .
WORKDIR "/src/WebAppPatientCare"
RUN dotnet build "WebAppPatientCare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppPatientCare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppPatientCare.dll"]