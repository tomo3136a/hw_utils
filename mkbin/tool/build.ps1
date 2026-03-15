#
#builder
#
param($OutputPath = "./bin", [switch]$Pass)

Write-Host "build start." -ForegroundColor Yellow

$Path = "src/*.cs"
$ReferencedAssemblies = "System.Configuration", "System.Windows.Forms", `
    "System.Drawing", "System.Xml", "System.Runtime.Serialization"

$EType=[Microsoft.PowerShell.Commands.OutputAssemblyType]
#$Type=$EType::ConsoleApplication
$Type=$EType::WindowsApplication

$AppName = Split-Path "." -Leaf
$AppExts = ".exe"

if (-not (Test-Path $OutputPath)) {
  New-Item -Path $OutputPath -ItemType Directory | Out-Null
  Write-Output "make directory."
}
$OutputAssembly = Join-Path (Resolve-Path -Path $OutputPath) $AppName$AppExts

Write-Output "build program:  $AppName$AppExts"
Write-Output "    Type:       $Type"
Write-Output "    Path:       $Path"
Write-Output "    Output:     $OutputAssembly"
Write-Output "    References: $ReferencedAssemblies"
Add-Type -Path $Path -OutputType $Type `
  -OutputAssembly $OutputAssembly `
  -ReferencedAssemblies $ReferencedAssemblies

Write-Host "build completed." -ForegroundColor Yellow
if (-not $Pass) { $host.UI.RawUI.ReadKey() | Out-Null }
