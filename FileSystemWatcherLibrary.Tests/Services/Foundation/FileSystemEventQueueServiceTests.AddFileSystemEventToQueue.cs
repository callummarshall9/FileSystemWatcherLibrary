using FileSystemWatcherLibrary.Models;
using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventQueueServiceTests
    {
        [Fact]
        public void AddFileSystemEventWorksAsExpected()
        {
            //given
            FileSystemEvent inputFileSystemEvent = GetRandomFileSystemEvent();

            //when
            fileSystemEventQueueService.AddFileSystemEventToQueue(inputFileSystemEvent);

            //then
            fileSystemEventQueueBrokerMock.Verify(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.AddFileSystemEventToQueue(inputFileSystemEvent), Times.Once());

            fileSystemEventQueueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
