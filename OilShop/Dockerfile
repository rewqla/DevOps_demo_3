# Use the ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["OilShop/OilShop.csproj", "OilShop/"]
RUN dotnet restore "OilShop/OilShop.csproj"
COPY . .
WORKDIR "/src/OilShop"
RUN dotnet build "OilShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OilShop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OilShop.dll"]
