using System;
using System.Collections.Generic;
using System.Linq;
using Guestbook.Domain.Entities;

namespace Guestbook.Domain.Repositories
{
    public class GuestbookEntryRepository : IGuestbookEntryRepository
    {
        private readonly List<GuestbookEntry> _entries = new List<GuestbookEntry> {
            new GuestbookEntry { GuestbookEntryId = 1, Author = "Steve", PostedDate = DateTime.Now, Comment = "I like this" },    
            new GuestbookEntry { GuestbookEntryId = 2, Author = "Bert", PostedDate = DateTime.Now, Comment = "I don't like it" },
        };

        public IQueryable<GuestbookEntry> Entries { 
            get {
                return _entries.AsQueryable();
            } 
        }

        public void AddEntry(GuestbookEntry entry)
        {
            entry.PostedDate = DateTime.Now;
            _entries.Add(entry);
        }
    }
}