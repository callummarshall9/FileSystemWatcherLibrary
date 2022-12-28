using System;

namespace FileSystemWatcherLibrary.Services.Orchestration
{
    public interface IFileSystemEventOrchestrationService
    {
        void ListenToEvents();
        void ListenToCreateEvents(Action<string> handler);
        void ListenToUpdateEvents(Action<string> handler);
        void ListenToDeleteEvents(Action<string> handler);
    }
}
