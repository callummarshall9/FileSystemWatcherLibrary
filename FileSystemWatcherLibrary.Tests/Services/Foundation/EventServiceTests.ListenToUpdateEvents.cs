using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void ListenToUpdateEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            eventService.ListenToUpdateEvents(handler);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.ListenToEvents("FileUpdateEvent", It.IsAny<Func<string, ValueTask>>()));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
