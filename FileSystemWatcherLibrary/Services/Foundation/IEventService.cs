using System;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public interface IEventService
    {
        public void ListenToCreateEvents(Action<string> handler);
        public void ListenToDeleteEvents(Action<string> handler);
        public void ListenToUpdateEvents(Action<string> handler);

        public void RaiseCreateEvent(string path);
        public void RaiseUpdateEvent(string path);
        public void RaiseDeleteEvent(string path);
    }
}
