using EventLibrary;
using System;
using System.Threading.Tasks;

namespace FileSystemWatcherLibrary.Brokers.Events
{
    public class EventLibraryBroker : IEventLibraryBroker
    {
        private readonly IEventHub eventLibrary;

        public EventLibraryBroker(IEventHub eventLibrary)
        {
            this.eventLibrary = eventLibrary;
        }

        public void ListenToEvents(string name, Func<string, ValueTask> handler)
            => eventLibrary.ListenToEvent(name, handler);

        public async ValueTask RaiseEventAsync(string name, string path)
            => await eventLibrary.RaiseEventAsync(name, path);
    }
}
