using System;
using System.IO;

namespace FileSystemWatcherLibrary.Brokers 
{
    public interface IFileSystemEventBroker 
    {
        public void ListenToEvents(Action<FileSystemEventArgs> handler);
    }
}