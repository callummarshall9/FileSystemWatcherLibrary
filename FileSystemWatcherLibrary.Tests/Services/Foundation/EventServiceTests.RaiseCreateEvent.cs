using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class EventServiceTests
    {
        [Fact]
        public void RaiseCreateEventWprksAsExpected()
        {
            //given
            string inputName = GetRandomString();

            //when
            eventService.RaiseCreateEvent(inputName);

            //then
            eventLibraryBroker.Verify(eventLibraryBroker =>
                eventLibraryBroker.RaiseEventAsync("FileCreateEvent", inputName));

            eventLibraryBroker.VerifyNoOtherCalls();
        }
    }
}
