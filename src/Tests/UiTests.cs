using NUnit.Framework;
using Epam.Automation.src.Business;
using Epam.Automation.src.Core;

namespace Epam.Automation.src.Tests
{
    [TestFixture]
    [Category("UI")]
    public class UiTests : BaseTest
    {
        [Test]
        public void HomePage_ShouldOpen_AndHaveCorrectTitle()
        {
            var homePage = new EpamHomePage(_driver);
            homePage.Open(ConfigManager.BaseUrl);
            Assert.That(_driver.Title, Is.EqualTo("EPAM | Software Engineering & Product Development Services"));
        }

        [Test]
        public void AboutPage_ShouldHaveTitle()
        {
            var homePage = new EpamHomePage(_driver);
            var aboutPage = new EpamAboutPage(_driver);
            homePage.Open(ConfigManager.BaseUrl + "/about");
            Assert.That(aboutPage.GetTitle(), Does.Contain("About"));
        }

        [Test]
        public void InsightsPage_ShouldShowHeader()
        {
            var homePage = new EpamHomePage(_driver);
            var insightsPage = new EpamInsightsPage(_driver);
            homePage.Open(ConfigManager.BaseUrl + "/insights");
            Assert.That(insightsPage.GetHeaderText(), Is.Not.Null.And.Not.Empty);
        }
    }
}