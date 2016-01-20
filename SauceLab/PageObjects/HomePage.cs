using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;

namespace SauceLab.PageObjects
{
    public class TestPage
    {
        protected IWebDriver Driver;

        public TestPage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
