using FileSystemWatcherLibrary.Brokers.Configuration;
using System;
using System.IO;

namespace FileSystemWatcherLibrary.Brokers 
{
    public class FileSystemEventBroker : IFileSystemEventBroker
    {
        private readonly IConfigurationBroker configuration;

        public FileSystemEventBroker(IConfigurationBroker configuration)
        {
            this.configuration = configuration;
        }

        public void ListenToEvents(Action<FileSystemEventArgs> handler)
        {
            var watcher = new FileSystemWatcher(configuration.GetLocalFolder());
            watcher.Created += (_, eventArgs) => handler(eventArgs);
            watcher.Deleted += (_, eventArgs) => handler(eventArgs);
            watcher.Changed += (_, eventArgs) => handler(eventArgs);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }
    }
}