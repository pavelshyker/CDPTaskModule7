using OpenQA.Selenium;

namespace TestWebProject.Webdriver
{
    public class BasePage
    {
        protected By TitleLocator;
        protected string title;

        protected BasePage(By TitleLocator, string title)
        {
            this.TitleLocator = TitleLocator;
            this.title = title;
            AssertIsOpen();
        }

        public void AssertIsOpen()
        {
            var label = new BaseElement(this.TitleLocator, this.title);
            label.WaitForIsVisible();
        }
    }
}