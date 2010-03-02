using System;
using System.Configuration;
using TechTalk.SpecFlow;
using Guestbook.Spec.Steps.Infrastructure;
using NUnit.Framework;
using WatiN.Core;

namespace Guestbook.Spec.Steps
{
    [Binding]
    class Navigation
    {
        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string relativeUrl)
        {
            var rootUrl = new Uri(ConfigurationManager.AppSettings["RootUrl"]);
            var absoluteUrl = new Uri(rootUrl, relativeUrl);
            WebBrowser.Current.GoTo(absoluteUrl);
        }

        [Then(@"I should be on the guestbook page")]
        public void ThenIShouldBeOnTheGuestbookPage()
        {
            Assert.That(WebBrowser.Current.Title, Is.EqualTo("Guestbook"));
        }

        [Given(@"I am on the guestbook page")]
        public void GivenIAmOnTheGuestbookPage()
        {
            WhenINavigateTo("/Guestbook");
        }

        [Then(@"I am on the posting page")]
        public void ThenIAmOnThePostingPage()
        {
            Assert.That(WebBrowser.Current.Title, Is.EqualTo("Guestbook : Post a New Entry"));
        }

        [Given(@"I am on the posting page")]
        public void GivenIAmOnThePostingPage()
        {
            GivenIAmOnTheGuestbookPage();
            WhenIClickTheButtonLabelled("Post a New Entry");
        }

        [Then(@"I should see a button labelled ""(.*)""")]
        public void ThenIShouldSeeAButtonLabelled(string label)
        {
            var matchingButtons = WebBrowser.Current.Buttons.Filter(Find.ByText(label));
            Assert.That(matchingButtons.Count, Is.EqualTo(1));
        }

        [When(@"I click the button labelled ""(.*)""")]
        public void WhenIClickTheButtonLabelled(string label)
        {
            WebBrowser.Current.Buttons.First(Find.ByText(label)).Click();
        }

        [Then(@"I see the flash message ""(.*)""")]
        public void ThenISeeTheFlashMessage(string message)
        {
            var flashElement = WebBrowser.Current.Element("flashMessage");
            Assert.That(flashElement.Text, Is.EqualTo(message));
        }

        [Then(@"I should see a field labelled ""(.*)""")]
        public void ThenIShouldSeeAFieldLabelled(string label)
        {
            var matchingFields = WebBrowser.Current.TextFields.Filter(Find.ByLabelText(label+":"));
            Assert.That(matchingFields.Count, Is.EqualTo(1));
        }

        [Given(@"I have filled out the form as follows")]
        public void GivenIHaveFilledOutTheFormAsFollows(TechTalk.SpecFlow.Table table)
        {
            foreach (var row in table.Rows)
            {
                var labelText = row["Label"] + ":";
                var value = row["Value"];
                WebBrowser.Current.TextFields.First(Find.ByLabelText(labelText)).TypeText(value);
            }
        }
    }
}
