using FileSystemWatcherLibrary.Brokers;
using System;
using System.IO;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public class FileSystemEventService : IFileSystemEventService
    {
        private readonly IFileSystemEventBroker broker;

        public FileSystemEventService(IFileSystemEventBroker broker)
        {
            this.broker = broker;
        }

        public void ListenToCreateEvents(Action<string> handler)
            => broker.ListenToEvents((eventArgs) => 
            {
                System.IO.FileInfo fi = new FileInfo(eventArgs.FullPath);

                if(eventArgs.ChangeType == WatcherChangeTypes.Created && fi.CreationTime.ToString() == fi.LastWriteTime.ToString())
                    handler(eventArgs.FullPath.Replace('\\', '/'));
            });

        public void ListenToUpdateEvents(Action<string> handler)
            => broker.ListenToEvents((eventArgs) => 
            {
                if(!(eventArgs.ChangeType == WatcherChangeTypes.Deleted))
                    handler(eventArgs.FullPath.Replace('\\', '/'));
            });

        public void ListenToDeleteEvents(Action<string> handler)
            => broker.ListenToEvents((eventArgs) => 
            {
                if(eventArgs.ChangeType == WatcherChangeTypes.Deleted)
                    handler(eventArgs.FullPath.Replace('\\', '/'));
            });
        
    }
}