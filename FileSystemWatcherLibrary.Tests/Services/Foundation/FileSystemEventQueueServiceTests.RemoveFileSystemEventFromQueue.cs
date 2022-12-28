using FileSystemWatcherLibrary.Models;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventQueueServiceTests
    {
        [Fact]
        public void RemoveFileSystemEventFromQueueWorksAsExpected()
        {
            //given
            FileSystemEvent inputFileSystemEvent = GetRandomFileSystemEvent();
            
            //when
            fileSystemEventQueueService.RemoveFileSystemEventFromQueue(inputFileSystemEvent);

            //then
            fileSystemEventQueueBrokerMock.Verify(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.RemoveFileSystemEventFromQueue(inputFileSystemEvent));

            fileSystemEventQueueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
