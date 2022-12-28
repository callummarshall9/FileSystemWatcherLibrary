using System;
using FileSystemWatcherLibrary.Services.Orchestration;

namespace FileSystemWatcherLibrary
{
    public class FileSystemWatcherLibrary : IFileSystemWatcherLibrary
    {
        private readonly IFileSystemEventOrchestrationService fileSystemEventService;

        public FileSystemWatcherLibrary(IFileSystemEventOrchestrationService fileSystemEventService)
        {
            this.fileSystemEventService = fileSystemEventService;
            fileSystemEventService.ListenToEvents();
        }

        public void ListenToCreateEvents(Action<string> handler)
            => fileSystemEventService.ListenToCreateEvents(handler);

        public void ListenToDeleteEvents(Action<string> handler)
            => fileSystemEventService.ListenToDeleteEvents(handler);

        public void ListenToUpdateEvents(Action<string> handler)
            => fileSystemEventService.ListenToUpdateEvents(handler);
    }
}
