del *.nupkg

cd ..\Specflow.Reporting
..\nuget\nuget.exe pack Specflow.Reporting.csproj -Build -Properties Configuration=Release -OutputDirectory ..\nuget

cd ..\plugins\Specflow.Reporting.PlainText
..\..\nuget\nuget.exe pack PlainText.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Json
..\..\nuget\nuget.exe pack Json.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Xml
..\..\nuget\nuget.exe pack Xml.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\Specflow.Reporting.Xml.NUnit
..\..\nuget\nuget.exe pack Xml.NUnit.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget -Version 0.0.1-alpha

cd ..\Specflow.Reporting.WebApp
..\..\nuget\nuget.exe pack WebApp.csproj -Build -Properties Configuration=Release -OutputDirectory ..\..\nuget

cd ..\..\nuget
