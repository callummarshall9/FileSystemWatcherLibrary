using FileSystemWatcherLibrary.Brokers.Events;
using System;

namespace FileSystemWatcherLibrary.Services.Foundation
{
    public class TimerEventService : ITimerEventService
    {
        private readonly ITimerEventBroker timerEventBroker;

        public TimerEventService(ITimerEventBroker timerEventBroker)
        {
            this.timerEventBroker = timerEventBroker;
        }

        public void ListenToTimerEvents(Action action)
            => timerEventBroker.ListenToTimerEvents(action);
    }
}
