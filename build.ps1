param (
    [switch]$noPause = $false
)
$ErrorActionPreference = "Stop"

Write-Host -------------------------------------------------------------------------------
Write-Host PACKING Facebook.Api.Client...
Write-Host -------------------------------------------------------------------------------


& dotnet pack ./src/Facebook.Api.Client/Facebook.Api.Client.csproj --output ./_builds


if (-not $noPause) {
    Write-Host "Successfully done! Your package is in './_builds' folder. Press any key to continue...`n" -NoNewLine -ForegroundColor Green
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
}
