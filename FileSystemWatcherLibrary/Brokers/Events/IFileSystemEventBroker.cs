using System;
using System.IO;

namespace FileSystemWatcherLibrary.Brokers 
{
    public interface IFileSystemEventBroker 
    {
        void ListenToEvents(Action<FileSystemEventArgs> handler);
    }
}