using FileSystemWatcherLibrary.Models;
using System.Collections.Generic;

namespace FileSystemWatcherLibrary.Brokers.Queues
{
    public interface IFileSystemEventQueueBroker
    {
        void AddFileSystemEventToQueue(FileSystemEvent fileSystemEvent);
        void RemoveFileSystemEventFromQueue(FileSystemEvent fileSystemEvent);
        IEnumerable<FileSystemEvent> GetFileSystemEvents();
        FileSystemEvent GetNextFileSystemEvent();
    }
}