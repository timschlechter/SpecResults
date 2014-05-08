del *.nupkg

nuget pack ..\Specflow.Reporting\Specflow.Reporting.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.PlainText\PlainText.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.Json\Json.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.Xml\Xml.csproj -Build -Properties Configuration=Release

nuget pack ..\plugins\Specflow.Reporting.Xml.NUnit\Xml.NUnit.csproj -Build -Properties Configuration=Release -Version 0.0.0-alpha

nuget pack ..\plugins\Specflow.Reporting.WebApp\WebApp.csproj -Build -Properties Configuration=Release -Version 0.0.0-alpha
