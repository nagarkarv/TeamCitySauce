using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SauceLab.SauceConfig
{
    [TestFixture]
    public class SauceBrowserConfig
    {
        /* Read Browser settings from App.config */
        private readonly string _browser = ConfigurationManager.AppSettings.Get("TestBrowser");
        private readonly string _version = ConfigurationManager.AppSettings.Get("TestVersion");
        private readonly string _os = ConfigurationManager.AppSettings.Get("TestOS");
        private readonly string _deviceName = ConfigurationManager.AppSettings.Get("DeviceName");
        private readonly string _deviceOrientation = ConfigurationManager.AppSettings.Get("DeviceOrientation");

        /* Sauce Labs Settings */
        private readonly string _username = ConfigurationManager.AppSettings.Get("SauceUsername"); //SAUCE_LABS_ACCOUNT_NAME
        private readonly string _accessKey = ConfigurationManager.AppSettings.Get("SauceAccessKey"); //SAUCE_LABS_ACCOUNT_KEY
        private readonly string _sauceUri = ConfigurationManager.AppSettings.Get("SauceUri"); // SAUCE_LABS_URI      

        /* Browser Capabilities */
        private DesiredCapabilities _caps;
        public DesiredCapabilities Caps
        {
            get { return _caps; }
            set { _caps = value; }
        }

        private IWebDriver _driver;

        public IWebDriver Driver
        {
            get { return _driver;}
            set { _driver = value; }
        }

        public SauceBrowserConfig()
        {
        }

        [SetUp]
        public void SetUpSauce()
        {
            _caps = new DesiredCapabilities();
            _caps.SetCapability(CapabilityType.BrowserName, _browser);
            _caps.SetCapability(CapabilityType.Version, _version);
            _caps.SetCapability(CapabilityType.Platform, _os);
            _caps.SetCapability("deviceName", _deviceName);
            _caps.SetCapability("deviceOrientation", _deviceOrientation);
    
            _caps.SetCapability("username", _username); //SAUCE_LABS_ACCOUNT_NAME
            _caps.SetCapability("accessKey", _accessKey); //SAUCE_LABS_ACCOUNT_KEY
            _caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            Console.WriteLine("Environment Combination = " + _caps.ToString() + "Platform = " + _os + "_caps Platform = " + _caps.Platform.MajorVersion);
            _driver = new RemoteWebDriver(new Uri(_sauceUri), _caps,
                TimeSpan.FromSeconds(600));
        }

        [TearDown]
        public void CleanUpSauce()
        {
            var passed = (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed);
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // Terminates the remote webdriver session
                Driver.Quit();
            }
        }
    }
}
