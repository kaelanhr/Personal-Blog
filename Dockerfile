# server
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-server
WORKDIR /app

# copy csproj and restore
COPY *.csproj ./Server/
COPY *.sln ./Server/
WORKDIR /app/Server
RUN dotnet restore

# copy remaining files and make a publish release
COPY . .
RUN dotnet publish -c Release

# run time
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
COPY --from=build-server /app/Server/bin/Release/net5.0/publish .
ENTRYPOINT [ "dotnet", "BlogWebsite.dll" ]