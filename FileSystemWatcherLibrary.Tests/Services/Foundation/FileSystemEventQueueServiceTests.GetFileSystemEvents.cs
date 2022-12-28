using FileSystemWatcherLibrary.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventQueueServiceTests
    {
        [Fact]
        public void GetEventsWorksAsExpected()
        {
            //given
            IEnumerable<FileSystemEvent> expectedFileSystemEvents = GetRandomFileSystemEventSet();

            fileSystemEventQueueBrokerMock.Setup(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.GetFileSystemEvents())
                .Returns(expectedFileSystemEvents);

            //when
            IEnumerable<FileSystemEvent> actualFileSystemEvents = fileSystemEventQueueService.GetFileSystemEvents();

            //then
            actualFileSystemEvents.Should().BeEquivalentTo(expectedFileSystemEvents);

            fileSystemEventQueueBrokerMock.Verify(fileSystemEventQueueBrokerMock =>
                fileSystemEventQueueBrokerMock.GetFileSystemEvents(), Times.Once());

            fileSystemEventQueueBrokerMock.VerifyNoOtherCalls();
        }
    }
}