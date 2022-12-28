using System;

namespace FileSystemWatcherLibrary.Brokers.Events
{
    public interface ITimerEventBroker
    {
        void ListenToTimerEvents(Action action);
    }
}
