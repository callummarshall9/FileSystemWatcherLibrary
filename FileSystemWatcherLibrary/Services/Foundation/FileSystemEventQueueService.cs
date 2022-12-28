using FileSystemWatcherLibrary.Brokers.Queues;
using FileSystemWatcherLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public class FileSystemEventQueueService : IFileSystemEventQueueService
    {
        private readonly IFileSystemEventQueueBroker fileSystemEventQueueBroker;

        public FileSystemEventQueueService(IFileSystemEventQueueBroker fileSystemEventQueueBroker)
        {
            this.fileSystemEventQueueBroker = fileSystemEventQueueBroker;
        }

        public void AddFileSystemEventToQueue(FileSystemEvent fileSystemEvent)
            => fileSystemEventQueueBroker.AddFileSystemEventToQueue(fileSystemEvent);

        public IEnumerable<FileSystemEvent> GetFileSystemEvents()
            => fileSystemEventQueueBroker.GetFileSystemEvents();

        public FileSystemEvent GetNextFileSystemEvent()
            => fileSystemEventQueueBroker.GetFileSystemEvents()
                .OrderBy(fse => fse.CreatedOn)
                .FirstOrDefault();

        public void RemoveFileSystemEventFromQueue(FileSystemEvent fileSystemEvent)
            => fileSystemEventQueueBroker.RemoveFileSystemEventFromQueue(fileSystemEvent);
    }
}
