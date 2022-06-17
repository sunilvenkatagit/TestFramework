using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestAutomationFramework.Actions
{
    public class PageActions
    {
        public static void Click(IWebDriver driver, By locator)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                driver.FindElement(locator).Click();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to click on the element.\n{ex.Message}");
            }
        }
        public static void ClickUsingJS(IWebDriver driver, By locator)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                ExecuteJavaScript(driver, locator, "arguments[0].click();");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to click on the element.\n{ex.Message}");
            }
        }
        public static void EnterText(IWebDriver driver, By locator, string text)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                driver.FindElement(locator).SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to enter text into the element.\n{ex.Message}");
            }
        }
        public static void EnterTextUsingJS(IWebDriver driver, By locator, string text)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                ExecuteJavaScript(driver, locator, $"arguments[0].value='{text}'");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to enter text into the element.\n{ex.Message}");
            }
        }
        public static string GetText(IWebDriver driver, By locator)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                return driver.FindElement(locator).Text;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to get text of the element.\n{ex.Message}");
            }
        }
        public static string GetTextUsingJS(IWebDriver driver, By locator)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                return ExecuteJavaScript(driver, locator, "return arguments[0].value;").ToString();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to get text of the element.\n{ex.Message}");
            }
        }
        public static void SelectDropdownByValue(IWebDriver driver, By locator, string value)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                new SelectElement(driver.FindElement(locator)).SelectByValue(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to select by value from dropdown.\n{ex.Message}");
            }
        }
        public static void SelectDropdownByText(IWebDriver driver, By locator, string value)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                new SelectElement(driver.FindElement(locator)).SelectByText(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to select by text from dropdown.\n{ex.Message}");
            }
        }
        public static void SelectDropdownByIndex(IWebDriver driver, By locator, int index)
        {
            ExplicitlyWaitForAnElement(driver, locator);

            try
            {
                new SelectElement(driver.FindElement(locator)).SelectByIndex(index);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to select by index from dropdown.\n{ex.Message}");
            }
        }
        public static void SwitchToWindowByIndex(IWebDriver driver, int index)
        {
            ExplicitlyWaitForNewWindow(driver);

            try
            {
                var windowsCollection = driver.WindowHandles;
                driver.SwitchTo().Window(windowsCollection[index]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to switch to a window with index number : '{index}'.\n{ex.Message}");
            }
        }
        public static void SwitchToWindowByName(IWebDriver driver, string name)
        {
            ExplicitlyWaitForNewWindow(driver);

            try
            {
                var windowsCollection = driver.WindowHandles;
                driver.SwitchTo().Window(name);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to switch to a window with name : '{name}'.\n{ex.Message}");
            }
        }

        private static IWebElement ExplicitlyWaitForAnElement(IWebDriver driver, By locator)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d =>
                {
                    var ele = d.FindElement(locator);
                    return ele.Displayed && ele.Enabled;
                });

                return driver.FindElement(locator);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Element is not found.\n{ex.Message}");
            }
        }
        private static void ExplicitlyWaitForNewWindow(IWebDriver driver)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d =>
                {
                    return d.WindowHandles.Count > 0;
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"There is no NEW window found to witch to.\n{ex.Message}");
            }
        }
        private static object ExecuteJavaScript(IWebDriver driver, By locator, string script)
        {
            try
            {
                return ((IJavaScriptExecutor)driver).ExecuteScript(script, driver.FindElement(locator));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to execute javaScript.\n{ex.Message}");
            }
        }
    }
}
