using NUnit.Framework;
using OpenQA.Selenium;
using Epam.Automation.src.Core;
using NUnit.Framework.Interfaces;

namespace Epam.Automation.src.Tests
{
    public abstract class BaseTest
    {

#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        protected IWebDriver _driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        [SetUp]
        public void SetUp()
        {
            var browser = ConfigManager.Instance.Get("Browser");
            _driver = DriverFactory.Instance.CreateDriver(browser);
            Logger.Info($"Browser started: {browser}");
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome.Status == TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    string screenshotDir = "Screenshots";
                    if (!Directory.Exists(screenshotDir))
                    {
                        Directory.CreateDirectory(screenshotDir);
                    }
                    string path = $"{screenshotDir}/error_{context.Test.Name}.png";
                    screenshot.SaveAsFile(path);
                    Logger.Error($"Test failed: Screenshot saved to {path}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Screenshot capture failed: {ex.Message}");
                }
            }
            _driver?.Quit();
            Logger.Info("Browser closed.");
        }
    }
}