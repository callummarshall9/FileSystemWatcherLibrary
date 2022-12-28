using System;
using Timer = System.Timers.Timer;

namespace FileSystemWatcherLibrary.Brokers.Events
{
    public class TimerEventBroker : ITimerEventBroker
    {
        private readonly Timer timer;
        private Action action;

        public TimerEventBroker()
        {
            timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(action != null)
                action();
        }

        public void ListenToTimerEvents(Action action)
        {
            this.action = action;
        }
    }
}
