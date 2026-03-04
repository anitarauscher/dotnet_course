using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Epam.Automation.src.Core
{
    public class DriverFactory
    {
        private static readonly DriverFactory _instance = new DriverFactory();
        public static DriverFactory Instance => _instance;

        public static IWebDriver GetDriver()
        {
            return Instance.CreateDriver("chrome");
        }

        public IWebDriver CreateDriver(string browser)
        {
            if (string.IsNullOrWhiteSpace(browser) || browser.ToLowerInvariant() == "chrome")
                return new ChromeDriver();
            if (browser.ToLowerInvariant() == "firefox")
                return new FirefoxDriver();
            return new ChromeDriver();
        }
    }
}