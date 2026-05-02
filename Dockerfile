# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EventServiceAPI/*.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app


# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT [ "dotnet", "EventServiceAPI.dll" ]