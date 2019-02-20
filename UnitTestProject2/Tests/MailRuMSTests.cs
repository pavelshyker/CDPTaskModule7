using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using TestWebProject.Forms;
using TestWebProject.Webdriver;

namespace TestWebProject
{
    [TestClass]
    public class Test : BaseTest
    {
        private const string LogIn = "testuser.19";
        private const string Password = "testCDP123";
        private const string EmailAddress = "litmarsd@mail.ru";
        private const string EmailSubject = "Subject";
        private const string EmailText = "EmailTestText";

        [TestMethod, TestCategory("Login")]
        public void LoginTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);

            Assert.IsTrue(startForm.LoginSuccessMarker());
        }

        [TestMethod, TestCategory("Login")]
        public void LoginTestUsingTabs()
        {
            var startForm = new StartPage();
            startForm.LoginUsingTabs(LogIn, Password);

            Assert.IsTrue(startForm.LoginSuccessMarker());
        }

        [TestMethod, TestCategory("EmailCreating")]
        public void CreateDraftEmailTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();

            Assert.AreEqual(EmailAddress, draftForm.GetEmailAddress());
        }

        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailAddressTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.AreEqual(EmailAddress, emailForm.GetDraftEmailAddress());
        }
        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailSubjectTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.AreEqual(EmailSubject, emailForm.GetDraftEmailSubject());
        }

        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailTextTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.IsTrue(emailForm.GetDraftEmailText().Contains(EmailSubject));
        }

        [TestMethod, TestCategory("EmailSending")]
        public void DraftFolderAfterSendingTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.DeleteAllDraft();
            draftForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            draftForm.OpenEmail();
            emailForm.SendEmail();
            emailForm.GoToDraftPage();

            Assert.IsFalse(draftForm.DraftEmailExist());
        }

        [TestMethod, TestCategory("EmailSending")]
        public void SendFolderAfterSendingTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToSentPage();
            var sentForm = new SentPage();
            sentForm.DeleteAllSent();
            sentForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();
            emailForm.SendEmail();
            emailForm.GoToSentPage();

            Assert.IsTrue(sentForm.SentEmailExist());
        }

        [TestMethod, TestCategory("Logout")]
        public void LogoutTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.LogOut();

            Assert.IsTrue(startForm.LogoutSuccessMarker());
        }

        [TestMethod, TestCategory("DeleteEmail")]
        public void DragAndDropEmailTest()
        {
            var startForm = new StartPage();
            startForm.Login(LogIn, Password);
            var inboxForm = new InboxPage();
            inboxForm.GoToSentPage();
            var sentForm = new SentPage();
            sentForm.DeleteAllSent();
            sentForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            emailForm.CreateANewEmail(EmailAddress, EmailSubject, EmailText);
            emailForm.SendEmail();
            emailForm.GoToSentPage();
            sentForm.DragAndDropFromSentToDelete();

            Assert.IsFalse(sentForm.SentEmailExist());
        }
    }
}
