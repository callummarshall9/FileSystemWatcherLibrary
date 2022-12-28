using FileSystemWatcherLibrary.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventQueueServiceTests
    {
        [Fact]
        public void GetNextFileSystemEventWorksAsExpected()
        {
            //given
            FileSystemEvent expectedFileSystemEvent = GetRandomFileSystemEvent();

            fileSystemEventQueueBrokerMock.Setup(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.GetNextFileSystemEvent())
                .Returns(expectedFileSystemEvent);

            //when
            FileSystemEvent actualFileSystemEvent = fileSystemEventQueueService.GetNextFileSystemEvent();

            //then
            actualFileSystemEvent.Should().BeEquivalentTo(expectedFileSystemEvent);

            fileSystemEventQueueBrokerMock.Verify(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.GetNextFileSystemEvent(), Times.Once());

            fileSystemEventQueueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
