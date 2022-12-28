using FileSystemWatcherLibrary.Services.Foundation;
using FileSystemWatcherLibrary.Services.Orchestration;
using Moq;
using Tynamix.ObjectFiller;

namespace FileSystemWatcherLibrary.Tests.Services.Orchestration
{
    public partial class FileSystemEventOrchestrationServiceTests
    {
        private readonly Mock<IFileSystemEventService> fileSystemEventServiceMock;
        private readonly Mock<IFileSystemEventQueueService> fileSystemEventQueueServiceMock;
        private readonly Mock<ITimerEventService> timerEventServiceMock;
        private readonly Mock<IEventService> eventServiceMock;
        private readonly IFileSystemEventOrchestrationService fileSystemEventOrchestrationService;

        public FileSystemEventOrchestrationServiceTests()
        {
            fileSystemEventServiceMock = new Mock<IFileSystemEventService>();
            fileSystemEventQueueServiceMock = new Mock<IFileSystemEventQueueService>();
            timerEventServiceMock = new Mock<ITimerEventService>();
            eventServiceMock = new Mock<IEventService>();
            fileSystemEventOrchestrationService = new FileSystemEventOrchestrationService(
                fileSystemEventServiceMock.Object,
                timerEventServiceMock.Object,
                fileSystemEventQueueServiceMock.Object,
                eventServiceMock.Object
            );
        }

        public string GetRandomString()
            => new Filler<string>().Create();
    }
}
