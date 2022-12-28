using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Orchestration
{
    public partial class FileSystemEventOrchestrationServiceTests
    {
        [Fact]
        public void ListenToCreateEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            fileSystemEventOrchestrationService.ListenToCreateEvents(handler);

            //then
            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.ListenToCreateEvents(handler));

            eventServiceMock.VerifyNoOtherCalls();
        }
    }
}
