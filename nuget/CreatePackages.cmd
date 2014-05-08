del *.nupkg

nuget pack ..\Specflow.Reporting\Specflow.Reporting.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.PlainText\Specflow.Reporting.PlainText.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.Json\Specflow.Reporting.Json.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.Xml\Specflow.Reporting.Xml.csproj -Build -Properties Configuration=Release
