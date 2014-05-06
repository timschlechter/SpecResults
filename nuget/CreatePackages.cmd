msbuild ..\SpecFlow.Reporting.sln /p:Configuration=NuGet

::------------------------------------------------------------------------------
:: Specflow.Reporting
::------------------------------------------------------------------------------
xcopy ..\Specflow.Reporting\bin\NuGet\Specflow.Reporting.* ..\Specflow.Reporting\nuget\lib\%1\ /Y
nuget pack ..\Specflow.Reporting\Specflow.Reporting.nuspec -BasePath ..\Specflow.Reporting\nuget

::------------------------------------------------------------------------------
:: Specflow.Reporting.PlainText
::------------------------------------------------------------------------------
xcopy ..\Specflow.Reporting.PlainText\bin\NuGet\Specflow.Reporting.PlainText.* ..\Specflow.Reporting.PlainText\nuget\lib\%1\ /Y
nuget pack ..\Specflow.Reporting.PlainText\Specflow.Reporting.PlainText.nuspec -BasePath ..\Specflow.Reporting.PlainText\nuget

::------------------------------------------------------------------------------
:: Specflow.Reporting.Json
::------------------------------------------------------------------------------
xcopy ..\Specflow.Reporting.Json\bin\NuGet\Specflow.Reporting.Json.* ..\Specflow.Reporting.Json\nuget\lib\%1\ /Y
nuget pack ..\Specflow.Reporting.Json\Specflow.Reporting.Json.nuspec -BasePath ..\Specflow.Reporting.Json\nuget

::------------------------------------------------------------------------------
:: Specflow.Reporting.Xml
::------------------------------------------------------------------------------
xcopy ..\Specflow.Reporting.Xml\bin\NuGet\Specflow.Reporting.Xml.* ..\Specflow.Reporting.Xml\nuget\lib\%1\ /Y
nuget pack ..\Specflow.Reporting.Xml\Specflow.Reporting.Xml.nuspec -BasePath ..\Specflow.Reporting.Xml\nuget
