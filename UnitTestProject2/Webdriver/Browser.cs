using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;

namespace TestWebProject.Webdriver
{
    public class Browser
    {
        private static Browser currentInstane;
        private static IWebDriver driver;
        public static BrowserTypes.BrowserType CurrentBrowser;
        public static int ImplWait;
        public static int TimeoutForElement;
        private static string browser;

        private Browser()
        {
            InitParamas();
            driver = BrowserTypes.GetDriver(CurrentBrowser, ImplWait);
        }

        private static void InitParamas()
        {
            ImplWait = Convert.ToInt32(Configuration.ElementTimeout);
            TimeoutForElement = Convert.ToInt32(Configuration.ElementTimeout);
            browser = Configuration.Browser;
            Enum.TryParse(browser, out CurrentBrowser);
        }

        public static Browser Instance => currentInstane ?? (currentInstane = new Browser());

        public static void WindowMaximise()
        {
            driver.Manage().Window.Maximize();
        }

        public static void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static IWebDriver GetDriver()
        {
            return driver;
        }

        public static void Quit()
        {
            driver.Quit();
            currentInstane = null;
            driver = null;
            browser = null;
        }
    }
}
