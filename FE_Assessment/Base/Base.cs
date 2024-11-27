using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using FE_Assessment.Utility;
using FE_Assessment.Actions;
using OpenQA.Selenium;
using System.Configuration;
using static FE_Assessment.Pages.HomePage;
using static FE_Assessment.Globals;
using FE_Assessment.Base;

namespace AventStack.ExtentReports
{
    public class BaseSetup
    {
        public ExtentReports extent;
        public ExtentTest test;
        public ThreadLocal<IWebDriver> driver = new();
        public ThreadLocal<ExtentTest> extent_test = new();
        
        [OneTimeSetUp]
        public void Setup()
        {
            var htmlreport = new ExtentSparkReporter(_reportDir + "index.html");
            extent = new ExtentReports();

            extent.AttachReporter(htmlreport);
            extent.AddSystemInfo("Enivorment", "QA");
        }

        [SetUp]
        public void Start_Browser()
        {
            extent_test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            var node = extent_test.Value.CreateNode("Setup");

            //Set Browser
            SetBrowser(node);

            //Read Data
            string? url = GetEnvironementData.GetEnvData(node);

            //Navigate to URL
            ElementActions.NavigateToUrl(driver.Value, url, add_btn, node);
            driver.Value.Manage().Window.Maximize();
        }

        public IWebDriver GetDriver()
        {
            return driver.Value;

        }

        private void SetBrowser(ExtentTest node)
        {
            string? RunEnivorment = ConfigurationManager.AppSettings["RunEnvironment"];

            if (RunEnivorment != null && RunEnivorment.Equals("Local"))
            {
                driver.Value = DriverSetup.LocalBrowserSetup(driver.Value);
                node.Log(Status.Pass, "Chromedriver setup for local browser");
            }
            else if (RunEnivorment != null && RunEnivorment.Equals("Remote"))
            {
                driver.Value = DriverSetup.RemoteBrowserSetup(driver.Value);
                node.Log(Status.Pass, "Chromedriver setup for remote browser");
            }
            else
            {
                TestContext.Progress.WriteLine("Please check browser name and run enivorment value in app.config file");

            }
        }

        [TearDown]
        public void SetTestResults()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                DateTime date = DateTime.Now;
                string Filename = "Screenshot_" + date.ToString("h_mm_ss") + ".png";
                extent_test?.Value?.Fail("TestCase Status : Failed", CaptureScreenShot(driver.Value, Filename));
                extent_test?.Value?.Fail(stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                extent_test.Value.Log(Status.Pass, MarkupHelper.CreateLabel("TestCase Status : " + status, ExtentColor.Green));
            }
            extent.Flush();
            driver.Value.Quit();
        }

        public static Model.Media CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }
}

