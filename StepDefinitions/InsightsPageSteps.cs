using TechTalk.SpecFlow;
using NUnit.Framework;
using Epam.Automation.src.Business;
using Epam.Automation.src.Core;
using OpenQA.Selenium;

namespace Epam.Automation.StepDefinitions
{
    [Binding]
    public class InsightsPageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private EpamInsightsPage? _insightsPage;
        private string? _headerText;

        public InsightsPageSteps(ScenarioContext scenarioContext)
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

        [Given(@"I open the insights page")]
        public void GivenIOpenTheInsightsPage()
        {
            _insightsPage = new EpamInsightsPage(_driver);
            _insightsPage.Open(ConfigManager.Instance.Get("BaseUrl") + "/insights");
            _headerText = _insightsPage.GetHeaderText();
        }

        [Then(@"the page header should contain ""(.*)""")]
        public void ThenThePageHeaderShouldContain(string expected)
        {
            Assert.That(_headerText, Does.Contain(expected));
        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver?.Quit();
        }
    }
}