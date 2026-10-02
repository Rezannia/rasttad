# مرحله ۱: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Rasttad.csproj", "./"]
RUN dotnet restore "Rasttad.csproj"

COPY . .
RUN dotnet publish "Rasttad.csproj" -c Release -o /app/publish

# مرحله ۲: Run
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Rasttad.dll"]