using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Epam.Automation.src.Business;
using Epam.Automation.src.Core;
using OpenQA.Selenium;

namespace Epam.Automation.StepDefinitions
{
    [Binding]
    public class HomePageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        protected IWebDriver _driver;

        private EpamHomePage? _homePage;
        private string? _actualTitle;

        public HomePageSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            if (!_scenarioContext.ContainsKey("driver"))
            {
                _driver = DriverFactory.GetDriver();
                _scenarioContext["driver"] = _driver;
            }
            else
            {
                _driver = (IWebDriver)_scenarioContext["driver"];
            }
        }

        [Given(@"I open the homepage")]
        public void GivenIOpenTheHomepage()
        {
            _homePage = new EpamHomePage(_driver);
            _homePage.Open(ConfigManager.Instance.Get("BaseUrl") ?? "");
            _actualTitle = _driver.Title;
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string expectedTitle)
        {
            Assert.That(_actualTitle, Is.EqualTo(expectedTitle));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }

    }

}