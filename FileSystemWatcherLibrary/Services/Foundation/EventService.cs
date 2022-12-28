using FileSystemWatcherLibrary.Brokers.Events;
using System;
using System.Threading.Tasks;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public class EventService : IEventService
    {
        private readonly IEventLibraryBroker eventLibraryBroker;

        public EventService(IEventLibraryBroker eventLibraryBroker)
        {
            this.eventLibraryBroker = eventLibraryBroker;
        }

        public void ListenToCreateEvents(Action<string> handler)
            => eventLibraryBroker.ListenToEvents("FileCreateEvent", (path) => {
                handler(path);
                return ValueTask.CompletedTask;
            });

        public void ListenToDeleteEvents(Action<string> handler)
            => eventLibraryBroker.ListenToEvents("FileDeleteEvent", (path) => {
                handler(path);
                return ValueTask.CompletedTask;
            });

        public void ListenToUpdateEvents(Action<string> handler)
            => eventLibraryBroker.ListenToEvents("FileUpdateEvent", (path) => {
                handler(path);
                return ValueTask.CompletedTask;
            });

        public void RaiseCreateEvent(string path)
            => eventLibraryBroker.RaiseEventAsync("FileCreateEvent", path);

        public void RaiseDeleteEvent(string path)
            => eventLibraryBroker.RaiseEventAsync("FileDeleteEvent", path);

        public void RaiseUpdateEvent(string path)
            => eventLibraryBroker.RaiseEventAsync("FileUpdateEvent", path);
    }
}
