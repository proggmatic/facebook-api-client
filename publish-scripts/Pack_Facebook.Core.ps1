param (
    [switch]$noPause = $false
)
$ErrorActionPreference = "Stop"
$currentLocation = Get-Location


#--- Set this variables ---#
Set-Location "../src/Facebook.Core"                                                  # relative to current location or full path
$buildDir = "../../builds"                                                           # relative to setted above new location


Write-Host -------------------------------------------------------------------------------
Write-Host PACKING Facebook.Core Release for .NET Core...
Write-Host -------------------------------------------------------------------------------


& dotnet pack --output $buildDir --configuration Release


Set-Location $currentLocation                                        # restore location
if (-not $noPause) {
    Write-Host "Successfully done! Press any key to continue...`n" -NoNewLine -ForegroundColor Green
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
}
