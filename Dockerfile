# API do SIGA para execução em container (servidor local do hospital).
#
# Os documentos de impressão (ficha, pré-anestésica e relatórios) são gerados como HTML pelas
# views Razor e renderizados no próprio tablet pelo visualizador interno do app. Por isso a
# imagem não precisa de Chromium, fontes nem bibliotecas nativas extras, e funciona sem internet.
#
#   docker build -t siga-api .
#   docker run -p 8080:8080 \
#     -e ConnectionStrings__postgresConnection="Host=...;Database=...;Username=...;Password=...;Search Path=siga_db" \
#     -e Jwt__Key="..." -e HospitalApi__BaseUrl="http://integrador/" siga-api

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish UFF.FichaAnestesica.Api/UFF.FichaAnestesica.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080 \
    TZ=America/Sao_Paulo
COPY --from=build /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "UFF.FichaAnestesica.Api.dll"]
