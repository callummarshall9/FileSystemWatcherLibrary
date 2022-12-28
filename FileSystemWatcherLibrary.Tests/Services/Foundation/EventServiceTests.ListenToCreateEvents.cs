using System;
using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void ListenToCreateEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            eventService.ListenToCreateEvents(handler);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.ListenToEvents("FileCreateEvent", It.IsAny<Func<string, ValueTask>>()));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
