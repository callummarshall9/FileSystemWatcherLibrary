using System;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public interface IFileSystemEventService
    {
        public void ListenToCreateEvents(Action<string> handler);
        public void ListenToUpdateEvents(Action<string> handler);
        public void ListenToDeleteEvents(Action<string> handler);
    }
}