@Plugin
Feature: Generate a WebApp out of SpecFlow test results
	Story
	=====
	In order to get testresults presented in a web application
	As a developer
	I want to learn about [SpecFlow.Reporting.WebApp](https://www.nuget.org/packages/SpecFlow.Reporting.WebApp/)

@Howto:install
Scenario: Learn how to install the SpecFlow.Reporting.WebApp package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecFlow.Reporting.WebApp" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecFlow.Reporting.WebApp"
	Then I can read the instructions on how to install the package