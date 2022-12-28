using FileSystemWatcherLibrary.Brokers;
using FileSystemWatcherLibrary.Services.Foundation;
using Moq;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventServiceTests
    {
        private readonly Mock<IFileSystemEventBroker> fileSystemEventBroker;
        private readonly IFileSystemEventService fileSystemEventService;

        public FileSystemEventServiceTests()
        {
            fileSystemEventBroker = new Mock<IFileSystemEventBroker>();
            fileSystemEventService = new FileSystemEventService(fileSystemEventBroker.Object);
        }
    }
}
