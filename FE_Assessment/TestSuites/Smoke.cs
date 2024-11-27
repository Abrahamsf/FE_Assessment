using AventStack.ExtentReports;
using NUnit.Framework;
using FE_Assessment.Pages;
using FE_Assessment.Utility;
using FE_Assessment.Actions;
using static FE_Assessment.Globals;
using Newtonsoft.Json.Linq;

namespace FE_Assessment.TestSuites
{
    [Parallelizable(ParallelScope.Children)]
    public class Smoke : BaseSetup
    {
        [Test, Category("Smoke")]
        public void TC001_AddFirstUser()
        {
            var driver = GetDriver();

            string? testCase = TestContext.CurrentContext.Test.MethodName;
            JObject testData = JObject.Parse(ReadTestData.GetTestData(testCase));


            CommonActions.AddUser_Action(driver, extent_test.Value, testData["TestCase"]);
            CommonActions.Search_Action(driver, extent_test.Value, testData["TestCase"]["FirstName"].ToString());
        }

        [Test, Category("Smoke")]
        public void TC002_AddSecondUser()
        {
            var driver = GetDriver();

            string? testCase = TestContext.CurrentContext.Test.MethodName;
            JObject testData = JObject.Parse(ReadTestData.GetTestData(testCase));


            CommonActions.AddUser_Action(driver, extent_test.Value, testData["TestCase"]);
            CommonActions.Search_Action(driver, extent_test.Value, testData["TestCase"]["FirstName"].ToString());
        }
    }
}
