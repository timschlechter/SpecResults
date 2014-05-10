@Plugin
Feature: Json reporting
	In order to get testresults in JSON format
	As a developer
	I want to learn about SpecFlow.Reporting.Json

@Howto:install
Scenario: Install the SpecFlow.Reporting.Json package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecFlow.Reporting.Json" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecFlow.Reporting.Json"
	Then I can read the instructions on how to install the package
