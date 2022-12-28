using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Orchestration
{
    public partial class FileSystemEventOrchestrationServiceTests
    {
        [Fact]
        public void ListenToUpdateEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            fileSystemEventOrchestrationService.ListenToUpdateEvents(handler);

            //then
            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.ListenToUpdateEvents(handler));

            eventServiceMock.VerifyNoOtherCalls();
        }
    }
}
