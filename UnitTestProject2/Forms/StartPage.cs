using OpenQA.Selenium;
using TestWebProject.Webdriver;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace TestWebProject.Forms
{
    public class StartPage : BasePage
    {
        private static readonly By StartPageLocator = By.XPath("//a[@id='PH_authLink']");

        public StartPage() : base(StartPageLocator, "Start Page")
        {

        }

        private readonly BaseElement loginButton = new BaseElement(By.XPath("//a[@id='PH_authLink']"));
        private readonly BaseElement signInFrame = new BaseElement(By.ClassName("ag-popup__frame__layout__iframe"));
        private readonly BaseElement loginInputField = new BaseElement(By.XPath("//input[@name='Login']"));
        private readonly BaseElement passwordInputField = new BaseElement(By.XPath("//input[@name='Password']"));
        private readonly BaseElement submitButton = new BaseElement(By.XPath("//button[@type='submit']"));
        private readonly BaseElement newEmailButton = new BaseElement(By.XPath("//div[@class = 'b-sticky']//a[@data-name='compose'] | //div[@class = 'b-sticky js-not-sticky']//a[@data-name='compose']"));

        private readonly BaseElement loginInputField2 = new BaseElement(By.XPath("//input[@id='mailbox:login']"));
        private readonly BaseElement passwordInputField2 = new BaseElement(By.XPath("//input[@id='mailbox:password']"));

        public void Login(string username, string password)
        {
            this.loginButton.JsHighlight();
            this.loginButton.JsClick();
            Browser.GetDriver().SwitchTo().Frame(this.signInFrame.GetElement());
            this.loginInputField.Click();
            this.loginInputField.SendKeys(username);
            this.passwordInputField.Click();
            this.passwordInputField.SendKeys(password);
            this.submitButton.Click();
            Browser.GetDriver().SwitchTo().DefaultContent();
        }

        public void LoginUsingTabs(string username, string password)
        {
            new Actions(Browser.GetDriver()).SendKeys(Keys.Tab).Build().Perform();
            new Actions(Browser.GetDriver()).SendKeys(Keys.Tab).Build().Perform();
            new Actions(Browser.GetDriver()).SendKeys(Keys.Tab).Build().Perform();
            this.loginInputField2.SendKeys(username);
            new Actions(Browser.GetDriver()).SendKeys(Keys.Tab).Build().Perform();
            new Actions(Browser.GetDriver()).SendKeys(Keys.Tab).Build().Perform();
            this.passwordInputField2.SendKeys(password);
            new Actions(Browser.GetDriver()).SendKeys(Keys.Enter).Build().Perform();
        }

        public bool LoginSuccessMarker()
        {
            return this.newEmailButton.Displayed;
        }

        public bool LogoutSuccessMarker()
        {
            return this.loginButton.Displayed;
        }

    }
}