using FileSystemWatcherLibrary.Models;
using System.Collections.Generic;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public interface IFileSystemEventQueueService
    {
        void AddFileSystemEventToQueue(FileSystemEvent fileSystemEvent);
        void RemoveFileSystemEventFromQueue(FileSystemEvent fileSystemEvent);
        IEnumerable<FileSystemEvent> GetFileSystemEvents();
        FileSystemEvent GetNextFileSystemEvent();
    }
}
