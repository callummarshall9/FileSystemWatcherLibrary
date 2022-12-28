using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void RaiseDeleteEventWprksAsExpected()
        {
            //given
            string inputName = GetRandomString();

            //when
            eventService.RaiseDeleteEvent(inputName);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.RaiseEventAsync("FileDeleteEvent", inputName));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
