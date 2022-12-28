using FileSystemWatcherLibrary.Brokers.Events;
using FileSystemWatcherLibrary.Services.Foundation;
using Moq;
using Tynamix.ObjectFiller;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        private readonly Mock<IEventLibraryBroker> eventLibraryBroker;
        private readonly IEventService eventService;

        public EventServiceTests()
        {
            eventLibraryBroker = new Mock<IEventLibraryBroker>();
            eventService = new EventService(eventLibraryBroker.Object);
        }

        public string GetRandomString()
            => new Filler<string>().Create();
    }
}
