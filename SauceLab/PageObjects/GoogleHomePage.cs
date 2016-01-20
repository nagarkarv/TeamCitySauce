using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace SauceLab.PageObjects
{
    class GoogleHomePage : TestPage
    {
        private const string HomePageUrl = "http://www.google.com";

        public GoogleHomePage(IWebDriver driver) : base(driver)
        {
        }

        public GoogleHomePage OpenGoogleHomePage()
        {
            Driver.Navigate().GoToUrl(HomePageUrl);
            return this;
        }

        public string GetPageTitle()
        {
            return Driver.Title;
        }

        public void SearchFor(string str)
        {
            IWebElement query = Driver.FindElement(By.Name("q"));
            query.SendKeys(str);
            query.Submit();
        }
    }
}
