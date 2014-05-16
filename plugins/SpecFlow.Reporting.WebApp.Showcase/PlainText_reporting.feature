@Plugin@Plugin
Feature: Plain Text reporting
	Story
	=====
	In order to get testresults in plain text format
	As a developer
	I want to learn about [SpecFlow.Reporting.PlainText](https://www.nuget.org/packages/SpecFlow.Reporting.PlainText/)

@Howto:install
Scenario: Learn how to install the SpecFlow.Reporting.PlainText package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecFlow.Reporting.PlainText" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecFlow.Reporting.PlainText"
	Then I can read the instructions on how to install the package