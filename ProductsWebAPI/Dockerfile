#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductsWebAPI/ProductsWebAPI.csproj", "ProductsWebAPI/"]
RUN dotnet restore "ProductsWebAPI/ProductsWebAPI.csproj"
COPY . .
WORKDIR "/src/ProductsWebAPI"
RUN dotnet build "ProductsWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductsWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductsWebAPI.dll"]