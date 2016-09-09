FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 5888/tcp

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5888"]
