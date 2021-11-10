# scss
FROM node:12-alpine AS build-scss
WORKDIR /app
RUN apk update
RUN npm install -g node-sass --unsafe-perm
COPY package.json ./Client/
COPY site.scss ./Client/
COPY ./scss ./Client/scss/
WORKDIR /app/Client
RUN npm run scss

# server
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-server
WORKDIR /app

# copy csproj and restore
COPY *.csproj ./Server/
COPY *.sln ./Server/
WORKDIR /app/Server
RUN dotnet restore

# copy remaining files and make a publish release
COPY . .
COPY --from=build-scss /app/Client/wwwroot /app/Server/wwwroot
RUN dotnet publish -c Release

# run time
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
RUN useradd -ms /bin/bash PersonalBlog && chown -R PersonalBlog /app
USER PersonalBlog
EXPOSE 80
EXPOSE 443
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
COPY --from=build-server /app/Server/bin/Release/net6.0/publish .
ENTRYPOINT [ "dotnet", "PersonalBlog.dll" ]