using System;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public interface ITimerEventService
    {
        void ListenToTimerEvents(Action action);
    }
}
