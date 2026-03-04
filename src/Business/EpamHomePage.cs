using OpenQA.Selenium;

namespace Epam.Automation.src.Business
{
    public class EpamHomePage(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        public void Open(string url)
        {
            Console.WriteLine("Navigating to: " + url);
            driver.Navigate().GoToUrl(url);
        }

        public void ClickAboutLink() =>
            driver.FindElement(By.LinkText("About")).Click();
    }
}