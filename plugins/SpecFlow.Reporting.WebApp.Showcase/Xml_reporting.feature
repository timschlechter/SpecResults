@Plugin@Plugin
Feature: XML reporting
	Story
	=====
	In order to get testresults in XML format
	As a developer
	I want to learn about [SpecFlow.Reporting.Xml](https://www.nuget.org/packages/SpecFlow.Reporting.Xml/)

@Howto:install
Scenario: Learn how to install the SpecFlow.Reporting.Xml package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecFlow.Reporting.Xml" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecFlow.Reporting.Xml"
	Then I can read the instructions on how to install the package