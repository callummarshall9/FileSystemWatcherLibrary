using FileSystemWatcherLibrary.Brokers.Events;
using FileSystemWatcherLibrary.Services.Foundation;
using Moq;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class TimerEventServiceTests
    {
        private readonly Mock<ITimerEventBroker> timerEventBrokerMock;
        private readonly ITimerEventService timerEventService;

        public TimerEventServiceTests()
        {
            timerEventBrokerMock = new Mock<ITimerEventBroker>();
            timerEventService = new TimerEventService(timerEventBrokerMock.Object);
        }
    }
}
