using FileSystemWatcherLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemWatcherLibrary.Brokers.Queues
{
    public class FileSystemEventQueueBroker : IFileSystemEventQueueBroker
    {
        private List<FileSystemEvent> fileSystemEventQueue;

        public FileSystemEventQueueBroker()
        {
            fileSystemEventQueue = new List<FileSystemEvent>();
        }

        public void AddFileSystemEventToQueue(FileSystemEvent fileSystemEvent)
            => fileSystemEventQueue.Add(fileSystemEvent);

        public IEnumerable<FileSystemEvent> GetFileSystemEvents()
            => fileSystemEventQueue.ToArray();

        public FileSystemEvent GetNextFileSystemEvent()
            => fileSystemEventQueue.LastOrDefault();

        public void RemoveFileSystemEventFromQueue(FileSystemEvent fileSystemEvent)
            => fileSystemEventQueue.Remove(fileSystemEvent);
    }
}
