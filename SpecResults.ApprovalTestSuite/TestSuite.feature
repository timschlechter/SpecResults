@Category:SomeFeatureCategory
Feature: Test Suite
	In order to test my reporter plugin
	As a developer
	I want to run a predefined test suite

@SomeTag
Scenario: Single scenario
	Given a scenario is specified
	When the tests run
	Then a report is generated

Scenario: Steps contain arguments
	Given a "awesome" scenario is specified
	When the tests with "multiple" parameters run "smoothly"
	Then "1" report is generated
	  
Scenario: Steps contain multi arguments
	Given a "awesome" scenario is specified with a multi-line argument
  """
  Here we go with mulitiple
  lines!
  """
	When the tests with multiple line parameters
  """
  like
  this 
  one
  """
	Then "1" report is generated
	  
Scenario: Step is not implemented
	Given a scenario is specified
	When a step is not implemented
	Then a report is generated

Scenario: Table param
	Given a scenario is specified
	When the a step contains a table and a "second" param:
	| Id | Name      |
	| 1  | John Doe  |
	| 2  | Some Dude |
	| 3  | Any Guy   |
	Then a report is generated

Scenario: Step uses method-name undescores style
	Given a scenario is specified
	When a step uses method-name style
	When a step uses method-name style with two parameters
	Then a report is generated

Scenario: Step uses method-name undescores style with table param
	Given a scenario is specified
	When a step uses method-name underscore style with a table param and a second param:
	| Id | Name      |
	| 1  | John Doe  |
	| 2  | Some Dude |
	| 3  | Any Guy   |
	Then a report is generated

Scenario: Nested step
	Given a scenario is specified
	When a child step was executed
	Then a report is generated

Scenario Outline: eating
  Given there are <start> cucumbers
  When I eat <eat> cucumbers
  Then I should have <left> cucumbers

  Examples:
    | start | eat | left |
    |  12   |  5  |  7   |
    |  20   |  5  |  15  |