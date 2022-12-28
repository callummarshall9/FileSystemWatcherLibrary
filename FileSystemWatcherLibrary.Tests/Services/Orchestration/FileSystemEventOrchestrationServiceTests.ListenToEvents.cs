using FileSystemWatcherLibrary.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Orchestration
{
    public partial class FileSystemEventOrchestrationServiceTests
    {
        [Fact]
        public void ListenToEventsHandlesFileSystemCreateEventsWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action<string> createEventAction = null;

            FileSystemEvent actualFileSystemEvent = null;
            FileSystemEvent expectedFileSystemEvent = new FileSystemEvent
            {
                ChangeKind = FileSystemEventEnum.Created,
                CreatedOn = DateTimeOffset.Now,
                Path = inputPath
            };

            fileSystemEventServiceMock.Setup((fileSystemEventServiceMock) =>
                fileSystemEventServiceMock.ListenToCreateEvents(It.IsAny<Action<string>>()))
                .Callback((Action<string> handler) => createEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(It.IsAny<FileSystemEvent>()))
                .Callback((FileSystemEvent fileSystemEvent) => actualFileSystemEvent = fileSystemEvent);

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            createEventAction(inputPath);

            //then
            expectedFileSystemEvent.CreatedOn = actualFileSystemEvent.CreatedOn;

            actualFileSystemEvent.Should().BeEquivalentTo(expectedFileSystemEvent);

            fileSystemEventServiceMock.Verify(fileSystemEventServiceMock =>
                fileSystemEventServiceMock.ListenToCreateEvents(createEventAction), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(actualFileSystemEvent), Times.Once());

            fileSystemEventQueueServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesFileSystemUpdateEventsWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action<string> changedEventAction = null;

            FileSystemEvent actualFileSystemEvent = null;
            FileSystemEvent expectedFileSystemEvent = new FileSystemEvent
            {
                ChangeKind = FileSystemEventEnum.Changed,
                CreatedOn = DateTimeOffset.Now,
                Path = inputPath
            };

            fileSystemEventServiceMock.Setup((fileSystemEventServiceMock) =>
                fileSystemEventServiceMock.ListenToUpdateEvents(It.IsAny<Action<string>>()))
                .Callback((Action<string> handler) => changedEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(It.IsAny<FileSystemEvent>()))
                .Callback((FileSystemEvent fileSystemEvent) => actualFileSystemEvent = fileSystemEvent);

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            changedEventAction(inputPath);

            //then
            expectedFileSystemEvent.CreatedOn = actualFileSystemEvent.CreatedOn;

            actualFileSystemEvent.Should().BeEquivalentTo(expectedFileSystemEvent);

            fileSystemEventServiceMock.Verify(fileSystemEventServiceMock =>
                fileSystemEventServiceMock.ListenToUpdateEvents(changedEventAction), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(actualFileSystemEvent), Times.Once());

            fileSystemEventQueueServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesFileSystemDeleteEventsWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action<string> deletedEventAction = null;

            FileSystemEvent actualFileSystemEvent = null;
            FileSystemEvent expectedFileSystemEvent = new FileSystemEvent
            {
                ChangeKind = FileSystemEventEnum.Deleted,
                CreatedOn = DateTimeOffset.Now,
                Path = inputPath
            };

            fileSystemEventServiceMock.Setup((fileSystemEventServiceMock) =>
                fileSystemEventServiceMock.ListenToDeleteEvents(It.IsAny<Action<string>>()))
                .Callback((Action<string> handler) => deletedEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(It.IsAny<FileSystemEvent>()))
                .Callback((FileSystemEvent fileSystemEvent) => actualFileSystemEvent = fileSystemEvent);

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            deletedEventAction(inputPath);

            //then
            expectedFileSystemEvent.CreatedOn = actualFileSystemEvent.CreatedOn;

            actualFileSystemEvent.Should().BeEquivalentTo(expectedFileSystemEvent);

            fileSystemEventServiceMock.Verify(fileSystemEventServiceMock =>
                fileSystemEventServiceMock.ListenToDeleteEvents(deletedEventAction), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.AddFileSystemEventToQueue(actualFileSystemEvent), Times.Once());

            fileSystemEventQueueServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsWhenHandlesTimerEventsDeletesFileSystemEventFromQueue()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;
            FileSystemEvent expectedFileSystemEvent = new FileSystemEvent
            {
                ChangeKind = FileSystemEventEnum.Created,
                CreatedOn = DateTimeOffset.Now,
                Path = inputPath
            };

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(expectedFileSystemEvent);

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(expectedFileSystemEvent), Times.Once());
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsWorksAsExpected()
        {
            //given
            Action timerEventAction = null;

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            fileSystemEventQueueServiceMock.VerifyNoOtherCalls();
            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsRaisesCreateEventWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Created,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseCreateEvent(inputPath), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsRaisesUpdateEventWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Changed,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseUpdateEvent(inputPath), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsRaisesDeleteEventWorksAsExpected()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Deleted,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseDeleteEvent(inputPath), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsNotRaisesCreateEventWhenLaterDeleted()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent deletedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Deleted,
                CreatedOn = DateTimeOffset.Now.AddHours(1),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { deletedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Created,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseCreateEvent(inputPath), Times.Never);

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsCreationEventRemoveEventsRelatedToPathWhenLaterDeleted()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent changedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Changed,
                CreatedOn = DateTimeOffset.Now.AddMinutes(30),
                Path = inputPath
            };

            FileSystemEvent deletedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Deleted,
                CreatedOn = DateTimeOffset.Now.AddHours(1),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { changedFileSystemEvent, deletedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Created,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseCreateEvent(inputPath), Times.Never);

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(changedFileSystemEvent), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(deletedFileSystemEvent), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsNotRaisesUpdateEventWhenLaterDeleted()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent deletedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Deleted,
                CreatedOn = DateTimeOffset.Now.AddHours(1),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { deletedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Changed,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseUpdateEvent(inputPath), Times.Never);

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsChangedEventRemoveEventsRelatedToPathWhenLaterDeleted()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent changedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Changed,
                CreatedOn = DateTimeOffset.Now.AddMinutes(30),
                Path = inputPath
            };

            FileSystemEvent deletedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Deleted,
                CreatedOn = DateTimeOffset.Now.AddHours(1),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { changedFileSystemEvent, deletedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Changed,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseUpdateEvent(inputPath), Times.Never);

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(changedFileSystemEvent), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(deletedFileSystemEvent), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsCreatedEventWithLaterChangeEventsBecomeSingularCreate()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent changedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Changed,
                CreatedOn = DateTimeOffset.Now.AddMinutes(30),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { changedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Created,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseCreateEvent(inputPath), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(changedFileSystemEvent), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ListenToEventsHandlesTimerEventsChangeEventWithLaterChangeEventsBecomeSingularChange()
        {
            //given
            string inputPath = GetRandomString();
            Action timerEventAction = null;

            FileSystemEvent changedFileSystemEvent = new FileSystemEvent()
            {
                ChangeKind = FileSystemEventEnum.Changed,
                CreatedOn = DateTimeOffset.Now.AddMinutes(30),
                Path = inputPath
            };

            timerEventServiceMock.Setup((timerEventServiceMock) =>
                timerEventServiceMock.ListenToTimerEvents(It.IsAny<Action>()))
                .Callback((Action handler) => timerEventAction = handler);

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents())
                .Returns(new[] { changedFileSystemEvent });

            fileSystemEventQueueServiceMock.Setup((fileSystemEventQueueServiceMock) =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent())
                .Returns(new FileSystemEvent
                {
                    ChangeKind = FileSystemEventEnum.Changed,
                    CreatedOn = DateTimeOffset.Now,
                    Path = inputPath
                });

            //when
            fileSystemEventOrchestrationService.ListenToEvents();
            timerEventAction();

            //then
            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetNextFileSystemEvent(), Times.Once());

            eventServiceMock.Verify(eventServiceMock =>
                eventServiceMock.RaiseUpdateEvent(inputPath), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.GetFileSystemEvents(), Times.Once());

            fileSystemEventQueueServiceMock.Verify(fileSystemEventQueueServiceMock =>
                fileSystemEventQueueServiceMock.RemoveFileSystemEventFromQueue(changedFileSystemEvent), Times.Once());

            eventServiceMock.VerifyNoOtherCalls();
        }
    }
}
