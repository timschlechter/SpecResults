Feature: Generate reports
	In order to get information about test results
	As a developer
	I want to generate a detailed report

Scenario: Single scenario
	Given a simple SpecFlow scenario was specified
	When I run the tests
	Then the report contains:
	| Type     | Title                                    |
	| Feature  | Generate reports                         |
	| Scenario | Single scenario                          |
	| Given    | a simple SpecFlow scenario was specified |
	| When     | I run the tests                          |
	| Then     | the report contains:                     |
