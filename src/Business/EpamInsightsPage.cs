using OpenQA.Selenium;

namespace Epam.Automation.src.Business
{
    public class EpamInsightsPage
    {
        private readonly IWebDriver _driver;

        public EpamInsightsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public string GetHeaderText()
        {
            return _driver.FindElement(By.TagName("h1")).Text;
        }

        public string GetTitle()
        {
            return _driver.Title;
        }
    }
}