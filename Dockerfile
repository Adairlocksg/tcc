# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de solution e projetos
COPY *.sln .
COPY TCC.Api/*.csproj ./TCC.Api/
COPY TCC.Application/*.csproj ./TCC.Application/
COPY TCC.Business/*.csproj ./TCC.Business/
COPY TCC.Data/*.csproj ./TCC.Data/
COPY TCC.Infra/*.csproj ./TCC.Infra/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código fonte
COPY . .

# Construir e publicar o projeto principal (assumindo que TCC.Api é o projeto de entrada)
WORKDIR /src/TCC.Api
RUN dotnet publish -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configurações para o Railway
ENV ASPNETCORE_URLS=http://+:${PORT}
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_VERSION=8.0.0

# Copiar os arquivos publicados
COPY --from=build /app/publish .

# Definir o comando de execução
ENTRYPOINT ["dotnet", "TCC.Api.dll"]