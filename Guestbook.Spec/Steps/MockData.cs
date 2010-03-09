using System;
using System.Linq;
using DeleporterCore.Client;
using Guestbook.Domain.Entities;
using Guestbook.Domain.Repositories;
using Guestbook.Spec.Steps.Infrastructure;
using Moq;
using TechTalk.SpecFlow;

namespace Guestbook.Spec.Steps
{
    [Binding]
    public class MockData
    {
        [Given(@"we have the following existing entries")]
        public void GivenWeHaveTheFollowingExistingEntries(Table table)
        {
            var tableSerialized = new SerializableTable(table);

            Deleporter.Run(() =>
            {
                var originalRepository = IoC.CurrentGuestbookEntryRepository;
                TidyUp.AddTask(() => { IoC.CurrentGuestbookEntryRepository = originalRepository; });

                var mockRepository = new Mock<IGuestbookEntryRepository>();
                mockRepository.Setup(x => x.Entries)
                    .Returns((from row in tableSerialized.Rows
                              select new GuestbookEntry
                              {
                                  Author = row["Name"],
                                  Comment = row["Comment"],
                                  PostedDate = Convert.ToDateTime(row["Posted date"])
                              }).AsQueryable());

                IoC.CurrentGuestbookEntryRepository = mockRepository.Object;
            });
        }
    }
}