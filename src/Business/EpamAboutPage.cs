using OpenQA.Selenium;

namespace Epam.Automation.src.Business
{
    public class EpamAboutPage
    {
        private readonly IWebDriver _driver;

        public EpamAboutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public string GetTitle()
        {
            return _driver.Title;
        }
    }
}