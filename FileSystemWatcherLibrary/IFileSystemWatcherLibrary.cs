using System;

namespace FileSystemWatcherLibrary
{
    public interface IFileSystemWatcherLibrary
    {
        void ListenToCreateEvents(Action<string> handler);
        void ListenToUpdateEvents(Action<string> handler);
        void ListenToDeleteEvents(Action<string> handler);
    }
}
