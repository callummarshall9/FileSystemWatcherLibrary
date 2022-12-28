using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Orchestration
{
    public partial class FileSystemEventOrchestrationServiceTests
    {
        [Fact]
        public void ListenToDeleteEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string _) => { };

            //when
            fileSystemEventOrchestrationService.ListenToDeleteEvents(handler);

            //then
            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.ListenToDeleteEvents(handler));

            eventServiceMock.VerifyNoOtherCalls();
        }
    }
}
