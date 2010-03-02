using Guestbook.Domain.Repositories;

namespace Guestbook
{
    /// <summary>
    /// Quick and simple stand-in for an IoC container. Perfectly good for this example.
    /// </summary>
    public static class IoC
    {
        public static IGuestbookEntryRepository CurrentGuestbookEntryRepository = new GuestbookEntryRepository();
    }
}