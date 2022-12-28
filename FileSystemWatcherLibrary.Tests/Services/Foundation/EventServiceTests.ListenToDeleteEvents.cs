using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void ListenToDeleteEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            eventService.ListenToDeleteEvents(handler);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.ListenToEvents("FileDeleteEvent", It.IsAny<Func<string, ValueTask>>()));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
