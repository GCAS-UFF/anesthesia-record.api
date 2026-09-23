Add-Type -Path 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.0\System.Data.Common.dll'
Add-Type -Path 'C:\Users\Mateus\Desktop\anesthesia-record.api\UFF.FichaAnestesica.Api\bin\Debug\net8.0\Microsoft.Data.Sqlite.dll'
$conn = New-Object Microsoft.Data.Sqlite.SqliteConnection('Data Source=c:\Users\Mateus\Desktop\anesthesia-record.api\UFF.FichaAnestesica.Api\SigaDb.sqlite')
$conn.Open()
$cmd = $conn.CreateCommand()
$cmd.CommandText = 'SELECT Id, Description FROM DrugCategories'
$reader = $cmd.ExecuteReader()
while($reader.Read()) {
    Write-Host $reader.GetInt32(0) '-' $reader.GetString(1)
}
