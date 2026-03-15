$app = "edif2xml"
$ras = "System.Configuration", "System.Xml", "System.Windows.Forms", "System.Drawing"
Add-Type -OutputType WindowsApplication -ReferencedAssemblies $ras -Path "./*.cs" `
  -OutputAssembly (Join-Path (Resolve-Path "..") "${app}.exe")
