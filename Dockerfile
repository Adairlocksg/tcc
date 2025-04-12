# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar solution e arquivos .csproj (etapa crítica para cache)
COPY ./TCC.sln .
COPY ./src/TCC.Api/*.csproj ./src/TCC.Api/
COPY ./src/TCC.Application/*.csproj ./src/TCC.Application/
COPY ./src/TCC.Business/*.csproj ./src/TCC.Business/
COPY ./src/TCC.Data/*.csproj ./src/TCC.Data/
COPY ./src/TCC.Infra/*.csproj ./src/TCC.Infra/

# 2. Restaurar dependências (cacheável graças à etapa anterior)
RUN dotnet restore "src/TCC.Api/TCC.Api.csproj"

# 3. Copiar todo o código fonte
COPY ./src ./src

# 4. Publicar (já inclui o build)
RUN dotnet publish "src/TCC.Api/TCC.Api.csproj" -c Release -o /app/publish --no-restore

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configurações para o Railway
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_VERSION=8.0.0

# Copiar os arquivos publicados
COPY --from=build /app/publish .

EXPOSE 8080

# Comando de execução
ENTRYPOINT ["dotnet", "TCC.Api.dll"]