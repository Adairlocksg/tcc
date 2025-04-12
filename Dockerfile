# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos da solução e do projeto
COPY ./TCC.sln ./
COPY ./src ./src

# Restaura os pacotes NuGet
RUN dotnet restore "src/TCC.Api/TCC.Api.csproj"

# Compila a solução
RUN dotnet build "src/TCC.Api/TCC.Api.csproj" -c Release -o /app/build

# Publica o projeto
RUN dotnet publish "src/TCC.Api/TCC.Api.csproj" -c Release -o /app/publish --no-restore

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Expõe a porta (ajuste conforme necessário)
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "TCC.Api.dll"]