using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class TimerEventServiceTests
    {
        [Fact]
        public void ListenToTimerEventsWorksAsExpected()
        {
            //given
            Action inputAction = () => { };

            //when
            timerEventService.ListenToTimerEvents(inputAction);

            //then
            timerEventBrokerMock.Verify(timerEventBrokerMock =>
                timerEventBrokerMock.ListenToTimerEvents(inputAction), Times.Once());

            timerEventBrokerMock.VerifyNoOtherCalls();
        }
    }
}
