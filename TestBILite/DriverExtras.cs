using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBILite
{
    
    public class DriverExtras
    {
        RemoteWebDriver driver;
        WebDriverWait wait;
        ChromeOptions options = new ChromeOptions();
        bool isDisposed = false;
        public DriverExtras()
        {
            options.AddAdditionalOption("se:recordVideo", true);
            driver = new RemoteWebDriver(new Uri("http://selenium-hub:4444/"), options);
            wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            driver.Manage().Window.Maximize(); 
        }

        public void NavigateTo(string url)
        {
            if (isDisposed == true)
            { 
                driver = new RemoteWebDriver(new Uri("http://selenium-hub:4444/"), options);
                isDisposed = false;
            }
            driver.Navigate().GoToUrl(url);
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public IWebElement FindElement(By elem)
        {
            return wait.Until(d => d.FindElement(elem));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By elem)
        {
            return driver.FindElements(elem);
        }

        public string GetText(By elem)
        {
            return wait.Until(d => d.FindElement(elem)).Text;
        }

        public IWebElement FindElementByClass(string classname, string divType)
        {
            return wait.Until(d => d.FindElement(By.XPath("//" + divType + "[@class = '" + classname + "']")));
        }

        public ReadOnlyCollection<IWebElement> FindElementsByClass(string classname, string divType)
        {
            return wait.Until(d => d.FindElements(By.XPath("//" + divType + "[@class = '" + classname + "']")));
        }

        public void ScrollDown(IWebElement element)
        {
            driver.ExecuteScript(string.Format("window.scrollTo({0}, {1})", element.Location.X - 300, element.Location.Y - 300));
            Thread.Sleep(500);
        }

        public void JavascriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }


        public void Dispose()
        {
            driver.Dispose();
            isDisposed = true;
        }

        public RemoteWebDriver Driver => driver;
    }
}
