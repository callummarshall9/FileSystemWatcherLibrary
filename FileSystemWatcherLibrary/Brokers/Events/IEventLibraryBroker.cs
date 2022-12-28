using System;
using System.Threading.Tasks;

namespace FileSystemWatcherLibrary.Brokers.Events
{
    public interface IEventLibraryBroker
    {
        public void ListenToEvents(string name, Func<string, ValueTask> handler);
        public ValueTask RaiseEventAsync(string name, string path);
    }
}
