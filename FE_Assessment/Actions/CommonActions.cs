using AventStack.ExtentReports;
using FE_Assessment.Pages;
using OpenQA.Selenium;

namespace FE_Assessment.Actions
{
    public class CommonActions : Exception
    {
        private static long lastTimeStamp = DateTime.UtcNow.Ticks;

        //Actions
        public static void AddUser_Action(IWebDriver driver, ExtentTest node, Newtonsoft.Json.Linq.JToken? testData)
        {
            var newnode = node.CreateNode("Add User");

            ElementActions.Click(driver, HomePage.add_btn, newnode, "Add User Button");
            ElementActions.InputText(driver, HomePage.firstname_input, testData["FirstName"].ToString(), newnode, "First Name Textbox");
            ElementActions.InputText(driver, HomePage.lastname_input, testData["LastName"].ToString(), newnode, "Last Name Textbox");
            ElementActions.InputText(driver, HomePage.username_input, testData["Username"].ToString() +"_"+ GetUniqueTimestamp, newnode, "User Name Textbox"); //Username made unique with timestamp
            ElementActions.InputText(driver, HomePage.password_input, testData["Password"].ToString(), newnode, "Password Textbox");

            //Select correct customer type based on data provided
            if (testData["Customer"].ToString().ToLower().Contains("aaa"))
                ElementActions.Click(driver, HomePage.customer_companyAAA_radiobtn, newnode, "Customer Company AAA RadioButton");
            else
                ElementActions.Click(driver, HomePage.customer_companyBBB_radiobtn, newnode, "Customer Company BBB RadioButton");

            //Select correct role based on data provided
            ElementActions.Click(driver, HomePage.role_dropdown, newnode, "Role Dropdown");
            switch (testData["Role"].ToString().ToLower())
            {
                case "admin":
                    ElementActions.Click(driver, HomePage.adminoption_role_dropdown, newnode, "Admin Option Role Dropdown");
                    break;
                case "customer":
                    ElementActions.Click(driver, HomePage.customeroption_role_dropdown, newnode, "Customer Option Role Dropdown");
                    break;
                default:
                    ElementActions.Click(driver, HomePage.salesoption_role_dropdown, newnode, "Sales Team Option Role Dropdown");
                    break;
            }

            ElementActions.InputText(driver, HomePage.email_input, testData["Email"].ToString(), newnode, "Email Textbox");
            ElementActions.InputText(driver, HomePage.cellphone_input, testData["Cell"].ToString(), newnode, "Cell Phone Textbox");

            //Add Screenshot to Report
            AddScreenshot(driver, newnode, "Add User Screen");

            ElementActions.Click(driver, HomePage.save_btn, newnode, "Save Button");
        }

        public static void Search_Action(IWebDriver driver, ExtentTest node, string search_txt)
        {
            var newnode = node.CreateNode("Search");

            ElementActions.InputText(driver, HomePage.search_input, search_txt, newnode, "Search Textbox");
            ElementActions.VerifyElementExist(driver, "//tr/td[contains(.,'" + search_txt + "')]", newnode, "First Name table data");

            //Add Screenshot to Report
            AddScreenshot(driver, newnode, "Searched Screen");
        }

        //Common Actions
        public static string GetUniqueTimestamp
        {
            get
            {
                long original, newValue;
                do
                {
                    original = lastTimeStamp;
                    long now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                                (ref lastTimeStamp, newValue, original) != original);

                return newValue.ToString();
            }
        }

        public static void AddScreenshot(IWebDriver driver, ExtentTest newnode, string text)
        {
            string Filename = "Screenshot_" + GetUniqueTimestamp + ".png";
            newnode.Log(Status.Pass, text, CaptureScreenShot(driver, Filename));
        }

        public static AventStack.ExtentReports.Model.Media CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }
}