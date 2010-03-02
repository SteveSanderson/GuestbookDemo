using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guestbook.Spec.Steps.Infrastructure;
using WatiN.Core;

namespace Guestbook.Spec.PageWrappers
{
    class GuestbookEntriesList
    {
        public static IEnumerable<Entry> DisplayedEntries
        {
            get
            {
                var entriesContainer = (IElementContainer)WebBrowser.Current.Element("entries");
                return from li in entriesContainer.ElementsWithTag("li")
                       select new Entry { Container = li as IElementContainer };
            }
        }

        public class Entry
        {
            public IElementContainer Container { get; internal set; }

            public string Author { get { return this["author"]; } }
            public string PostedDate { get { return this["postedDate"]; } }
            public string Comment { get { return this["comment"]; } }

            public string this[string key]
            {
                get { return Container.Element(Find.ByClass(key)).Text; }
            }
        }
    }
}
