using System;
using System.Collections.Generic;
using System.Linq;
using Guestbook.Spec.PageWrappers;
using Guestbook.Spec.Steps.Infrastructure;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Guestbook.Spec.Steps
{
    [Binding]
    public class ViewingGuestbook
    {
        [Then(@"I should see a list of guestbook entries")]
        public void ThenIShouldSeeAListOfGuestbookEntries()
        {
            Assert.IsTrue(GuestbookEntriesList.DisplayedEntries.Any());
        }

        [Then(@"guestbook entries have an? (.*)")]
        public void ThenGuestbookEntriesHaveA(string propertyName)
        {
            var entries = GuestbookEntriesList.DisplayedEntries;
            switch (propertyName)
            {
                case "author": AssertNotAllAreEmpty(entries.Select(x => x.Author)); break;
                case "comment": AssertNotAllAreEmpty(entries.Select(x => x.Comment)); break;
                case "posted date": AssertNotAllAreEmpty(entries.Select(x => x.PostedDate)); break;
                default: throw new ArgumentException("Unknown property", "propertyName");
            }
        }

        [Then(@"the guestbook entries includes the following")]
        public void ThenTheGuestbookEntriesIncludesTheFollowing(Table table)
        {
            var entries = GuestbookEntriesList.DisplayedEntries.ToList();
            foreach (var row in table.Rows) {
                var matchingEntries = from entry in entries
                                      where entry.Author == row["Name"]
                                            && entry.Comment == row["Comment"]
                                            && IsValidPostedDate(entry, row["Posted date"])
                                      select entry;
                CollectionAssert.IsNotEmpty(matchingEntries);
            }
        }

        private static bool IsValidPostedDate(GuestbookEntriesList.Entry entry, string postedDateSpecifier)
        {
            switch (postedDateSpecifier)
            {
                case "(within last minute)":
                    return DateTime.Now.Subtract(DateTime.Parse(entry.PostedDate)).TotalSeconds < 60;
                default: return Convert.ToDateTime(postedDateSpecifier) == Convert.ToDateTime(entry.PostedDate);
            }
        }

        private static void AssertNotAllAreEmpty(IEnumerable<string> strings)
        {
            CollectionAssert.IsNotEmpty(strings.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}