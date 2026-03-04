using TechTalk.SpecFlow;
using NUnit.Framework;
using Epam.Automation.src.Business;
using Epam.Automation.src.Core;
using OpenQA.Selenium;

namespace Epam.Automation.StepDefinitions
{
    [Binding]
    public class AboutPageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        private EpamAboutPage? _aboutPage;
        private string? _actualTitle;

        public AboutPageSteps(ScenarioContext scenarioContext)
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

        [Given(@"I open the about page")]
        public void GivenIOpenTheAboutPage()
        {
            _aboutPage = new EpamAboutPage(_driver);
            _aboutPage.Open(ConfigManager.Instance.Get("BaseUrl") + "/about");
            _actualTitle = _driver.Title;
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenThePageTitleShouldContain(string expected)
        {
            Assert.That(_actualTitle, Does.Contain(expected));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }

    }
}