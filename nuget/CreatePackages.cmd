del *.nupkg

cd ..\Specflow.Reporting
nuget pack Specflow.Reporting.csproj -Build -Properties Configuration=Release -OutputDirectory ..\nuget

cd ..\plugins\Specflow.Reporting.PlainText
nuget pack PlainText.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Json
nuget pack Json.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Xml
nuget pack Xml.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Xml.NUnit
nuget pack Xml.NUnit.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget -Version 0.0.1-alpha

cd ..\Specflow.Reporting.WebApp
nuget pack WebApp.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget
