using System;

namespace Guestbook.Domain.Entities
{
    public class GuestbookEntry
    {
        public int GuestbookEntryId { get; set; }
        public string Author { get; set; }
        public DateTime PostedDate { get; set; }
        public string Comment { get; set; }
    }
}