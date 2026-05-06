# =========================================
# DEPLOY AUTOMÁTICO .NET → AZURE (ZIP DEPLOY)
# =========================================

$solutionRoot = "E:\uff_trabalhos\fa_uff\api"

# 🔥 IMPORTANTE: garanta o caminho real do csproj
$projectPath = Join-Path $solutionRoot "UFF.FichaAnestesica.Api\UFF.FichaAnestesica.Api.csproj"

$outputPath = Join-Path $solutionRoot "publish"
$zipPath = Join-Path $solutionRoot "app.zip"

$appName = "anesthesia-record-api-dzcsd2eqgybbhmdd"

$username = '$anesthesia-record-api'
$password = '5EY6bjtsXbcvuguwcv1uCRf4MSY2sxGfphbjc7dySjfnKYZccEnhCGfNaXgJ'

Write-Host "🧹 Limpando..." -ForegroundColor Yellow

Remove-Item $outputPath -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item $zipPath -Force -ErrorAction SilentlyContinue

Write-Host "📦 Publish iniciando..." -ForegroundColor Cyan

# 🔥 FIX IMPORTANTE: captura saída corretamente
$publishResult = & dotnet publish $projectPath -c Release -o $outputPath 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ FALHA NO PUBLISH:" -ForegroundColor Red
    Write-Host $publishResult
    exit 1
}

if (!(Test-Path $outputPath)) {
    Write-Host "❌ Publish não gerou pasta output!" -ForegroundColor Red
    exit 1
}

Write-Host "🗜️ Criando ZIP..." -ForegroundColor Cyan

Compress-Archive -Path "$outputPath\*" -DestinationPath $zipPath -Force

if (!(Test-Path $zipPath)) {
    Write-Host "❌ ZIP não foi criado!" -ForegroundColor Red
    exit 1
}

Write-Host "🚀 Deploy Azure..." -ForegroundColor Cyan

$zipFullPath = (Resolve-Path $zipPath).Path

$deployResult = & curl.exe -X POST "https://$appName.scm.brazilsouth-01.azurewebsites.net/api/zipdeploy" `
  -u "${username}:${password}" `
  --data-binary "@$zipFullPath" `
  --fail `
  --show-error 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ ERRO NO DEPLOY:" -ForegroundColor Red
    Write-Host $deployResult
    exit 1
}

Write-Host "✅ DEPLOY CONCLUÍDO!" -ForegroundColor Green
Write-Host "🌐 https://$appName.brazilsouth-01.azurewebsites.net/"