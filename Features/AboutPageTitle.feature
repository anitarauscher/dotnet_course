Feature: About Page Title
  Scenario: Open about page and verify title
    Given I open the about page
    Then the page title should contain "About"
    