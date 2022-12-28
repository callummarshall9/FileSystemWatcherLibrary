using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void RaiseUpdateEventWprksAsExpected()
        {
            //given
            string inputName = GetRandomString();

            //when
            eventService.RaiseUpdateEvent(inputName);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.RaiseEventAsync("FileUpdateEvent", inputName));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
