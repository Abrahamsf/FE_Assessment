using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FE_Assessment.Actions
{
    public class ElementActions
    {

        public static void WaitForPageLoad(IWebDriver driver, int timeout = 5)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public static IWebElement WaitForElementToDisplay(IWebDriver driver, string elementLocator, int timeout = 5)
        {
            DefaultWait<IWebDriver> fluentwait = new DefaultWait<IWebDriver>(driver);
            fluentwait.Timeout = TimeSpan.FromSeconds(timeout);
            fluentwait.PollingInterval = TimeSpan.FromSeconds(3);
            fluentwait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            fluentwait.Message = "Element not found";

            IWebElement element = driver.FindElement(By.XPath(elementLocator));
            return element;
        }

        public static IWebElement FindElement(IWebDriver driver, string elementLocator, int timeout = 5)
        {
            IWebElement element = null;

            try
            {
                element = WaitForElementToDisplay(driver, elementLocator, timeout);
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine("Stalement Element expection occured, re-trying to find element");
                    WaitForPageLoad(driver, timeout);
                    element = WaitForElementToDisplay(driver, elementLocator, timeout);
                }
                catch (Exception e1)
                {

                    Console.Write(e1.Message);
                }
            }
            catch (NoSuchElementException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return element;
        }

        public static void ScrollToView(IWebDriver driver, IWebElement element)
        {
            /*
             * If this method is not working, use following code
             * ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
             */
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoViewIfNeeded()", element);

        }

        public static void Click(IWebDriver driver, string elementLocator, ExtentTest node, string elementName="", int timeout = 5)
        {

            try
            {
                IWebElement element = WaitForElementToDisplay(driver, elementLocator, timeout);
                if (element != null)
                {
                    ScrollToView(driver, element);
                    element.Click();
                    node.Log(Status.Pass, elementName + " - Element clicked successfully");
                }
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine("Stale element expection occured, re-trying to perform Click action");
                    IWebElement element = WaitForElementToDisplay(driver, elementLocator, timeout);
                    ScrollToView(driver, element);
                    element.Click();
                    node.Log(Status.Pass, elementName+" - Element clicked successfully");
                }
                catch (Exception e1)
                {
                    Console.Write(e1.Message);
                    node.Log(Status.Fail, elementName + " - Element NOT clicked");
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                node.Log(Status.Fail, elementName + " - Element NOT clicked");
            }
        }

        public static void SelectDropDownByValue(IWebDriver driver, string elementLocator, string value, int timeout)
        {
            try
            {
                SelectElement select = new SelectElement(FindElement(driver, elementLocator, timeout));
                select.SelectByValue(value);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to select value from dropdown");
            }
        }

        public static void MouseOver(IWebDriver driver, string elementLocator, int timeout = 5)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            action.MoveToElement(FindElement(driver, elementLocator)).Perform();
        }

        public static void NavigateToUrl(IWebDriver driver, string URL, string ele, ExtentTest node)
        {
            try
            {
                driver.Navigate().GoToUrl(URL);
                node.Log(Status.Pass, "Navigated to - " + URL + " - successfully");
            }
            catch
            {
                WaitForElement(driver, ele);
            }
        }

        public static void SendKeys(IWebDriver driver, string elementLocator, string value)
        {
            IWebElement element = WaitForElementToDisplay(driver, elementLocator);
            if (element != null)
            {
                element.Clear();
                element.SendKeys(value);          }
        }

        public static bool ElementExist(IWebDriver driver, string elementLocator)
        {
            try
            {
                driver.FindElement(By.XPath(elementLocator));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void WaitForElement(IWebDriver driver, string elementLocator)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                if (ElementExist(driver, elementLocator))
                {
                    break;
                }
            }
        }

        public static void InputText(IWebDriver driver, string elementLocator, string text, ExtentTest newnode, string elementName="")
        {
            WaitForElement(driver, elementLocator);

            driver.FindElement(By.XPath(elementLocator)).Click();
            driver.FindElement(By.XPath(elementLocator)).SendKeys(text);
            newnode.Log(Status.Pass, text + "- successfully inserted into -" + elementName + " - Element");
        }

        public static void VerifyElementExist(IWebDriver driver, string elementLocator, ExtentTest newnode, string elementName = "")
        {
            WaitForElement(driver, elementLocator);
            if (ElementExist(driver, elementLocator))
            {
                newnode.Log(Status.Pass, elementName + " - Element found");
            }
            else
            {
                newnode.Log(Status.Fail, elementName + " - Element NOT found with element locator - " + elementLocator);
            }
        }
    }
}
