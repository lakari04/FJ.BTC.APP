FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FJ.BTC.APP/FJ.BTC.APP.csproj", "FJ.BTC.APP/"]
RUN dotnet restore "FJ.BTC.APP/FJ.BTC.APP.csproj"
COPY . .
WORKDIR "/src/FJ.BTC.APP"
RUN dotnet build "FJ.BTC.APP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FJ.BTC.APP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FJ.BTC.APP.dll"]
