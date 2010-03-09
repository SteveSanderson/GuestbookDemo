using System;
using System.Collections.Generic;
using System.Linq;
using Guestbook.Spec.PageWrappers;
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

        [Then(@"the guestbook entries includes the following(, in this order)?")]
        public void ThenTheGuestbookEntriesIncludesTheFollowing(string inThisOrder, Table table)
        {
            bool requiresExactOrder = !string.IsNullOrEmpty(inThisOrder);
            List<GuestbookEntriesList.Entry> entries = GuestbookEntriesList.DisplayedEntries.ToList();

            Func<GuestbookEntriesList.Entry, TableRow, bool> equalityComparer = (entry, row) => {
                return (row["Name"] == entry.Author
                        && row["Comment"] == entry.Comment
                        && IsValidPostedDate(entry, row["Posted date"]));
            };

            int lastMatchIndex = -1;
            foreach (var row in table.Rows) {
                lastMatchIndex = entries.FindIndex(requiresExactOrder ? lastMatchIndex + 1 : 0, entry => equalityComparer(entry, row));
                if (lastMatchIndex < 0)
                    Assert.Fail("No match for " + row);
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