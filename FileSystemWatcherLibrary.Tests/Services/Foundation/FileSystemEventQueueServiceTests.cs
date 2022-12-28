using FileSystemWatcherLibrary.Brokers.Queues;
using FileSystemWatcherLibrary.Models;
using FileSystemWatcherLibrary.Services.Foundation;
using Moq;
using System;
using Tynamix.ObjectFiller;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventQueueServiceTests
    {
        private readonly Mock<IFileSystemEventQueueBroker> fileSystemEventQueueBrokerMock;
        private readonly IFileSystemEventQueueService fileSystemEventQueueService;

        public FileSystemEventQueueServiceTests()
        {
            fileSystemEventQueueBrokerMock = new Mock<IFileSystemEventQueueBroker>();
            fileSystemEventQueueService = new FileSystemEventQueueService(fileSystemEventQueueBrokerMock.Object);
        }

        public Filler<FileSystemEvent> GetFileSystemEventFiller()
        {
            var filler = new Filler<FileSystemEvent>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.Now);

            return filler;
        }

        public FileSystemEvent GetRandomFileSystemEvent()
            => GetFileSystemEventFiller().Create();

        public IEnumerable<FileSystemEvent> GetRandomFileSystemEventSet()
            => Enumerable.Range(0, new Random().Next(1, 10))
                .Select(c => GetRandomFileSystemEvent())
                .ToArray();
    }
}
