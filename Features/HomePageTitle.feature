Feature: Home Page Title
  In order to verify EPAM Homepage title
  As a user
  I want to see the correct page title when I open the main page

  Scenario: Open homepage and check title
    Given I open the homepage
    Then the page title should be "EPAM | Software Engineering & Product Development Services"