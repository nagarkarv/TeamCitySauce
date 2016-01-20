using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SauceLab.PageObjects;
using SauceLab.SauceConfig;

namespace SauceLab.Test
{
    public class SauceLabTest : SauceBrowserConfig
    {
        private GoogleHomePage _googleHomePage;

        public SauceLabTest()
        {
            
        }

        private void InitalizePageObjects()
        {
            _googleHomePage = new GoogleHomePage(Driver);
        }

        [SetUp]
        public void SetUp()
        {
            InitalizePageObjects();
        }

        [Test]
        public void Test_OpenGoogle_Find_Github_ReturnsSuccessFailure()
        {
            _googleHomePage.OpenGoogleHomePage();
            StringAssert.Contains("Google", _googleHomePage.GetPageTitle());
            _googleHomePage.SearchFor("github");
        }

        [Test]
        public void Test_OpenGoogle_Find_ASPDOTNET_ReturnsSuccessFailure()
        {
            _googleHomePage.OpenGoogleHomePage();
            StringAssert.Contains("ASP.NET", _googleHomePage.GetPageTitle());
            _googleHomePage.SearchFor("ASP.NET");
        }

        [Test]
        public void Test_OpenGoogle_Find_Agile_ReturnsSuccessFailure()
        {
            _googleHomePage.OpenGoogleHomePage();
            StringAssert.Contains("Google", _googleHomePage.GetPageTitle());
            _googleHomePage.SearchFor("Agile");
        }

        [TearDown]
        public void CleanUp()
        {
        }
    }
}
