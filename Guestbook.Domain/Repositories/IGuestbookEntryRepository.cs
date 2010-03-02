using System.Linq;
using Guestbook.Domain.Entities;

namespace Guestbook.Domain.Repositories
{
    public interface IGuestbookEntryRepository
    {
        IQueryable<GuestbookEntry> Entries { get; }
        void AddEntry(GuestbookEntry entry);
    }
}