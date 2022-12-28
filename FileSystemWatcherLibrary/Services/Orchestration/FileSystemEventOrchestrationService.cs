using FileSystemWatcherLibrary.Models;
using FileSystemWatcherLibrary.Services.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemWatcherLibrary.Services.Orchestration
{
    public class FileSystemEventOrchestrationService : IFileSystemEventOrchestrationService
    {
        private readonly IFileSystemEventService fileSystemEventService;
        private readonly ITimerEventService timerEventService;
        private readonly IFileSystemEventQueueService fileSystemEventQueueService;
        private readonly IEventService eventService;

        public FileSystemEventOrchestrationService(IFileSystemEventService fileSystemEventService, 
            ITimerEventService timerEventService, 
            IFileSystemEventQueueService fileSystemEventQueueService,
            IEventService eventService)
        {
            this.fileSystemEventService = fileSystemEventService;
            this.timerEventService = timerEventService;
            this.fileSystemEventQueueService = fileSystemEventQueueService;
            this.eventService = eventService;
        }

        public void ListenToEvents()
        {
            fileSystemEventService.ListenToCreateEvents((path) =>
                fileSystemEventQueueService.AddFileSystemEventToQueue(new FileSystemEvent
                {
                    Path = path,
                    ChangeKind = FileSystemEventEnum.Created,
                    CreatedOn = System.DateTimeOffset.Now
                }
             ));

            fileSystemEventService.ListenToUpdateEvents((path) =>
                fileSystemEventQueueService.AddFileSystemEventToQueue(new FileSystemEvent
                {
                    Path = path,
                    ChangeKind = FileSystemEventEnum.Changed,
                    CreatedOn = System.DateTimeOffset.Now
                }
            ));

            fileSystemEventService.ListenToDeleteEvents((path) =>
                fileSystemEventQueueService.AddFileSystemEventToQueue(new FileSystemEvent
                {
                    Path = path,
                    ChangeKind = FileSystemEventEnum.Deleted,
                    CreatedOn = System.DateTimeOffset.Now
                }
            ));

            timerEventService.ListenToTimerEvents(HandleTimerEvent);
        }

        private void HandleTimerEvent()
        {
            var nextEvent = fileSystemEventQueueService.GetNextFileSystemEvent();
            if (nextEvent == null) return;

            IEnumerable<FileSystemEvent> eventsRelated = fileSystemEventQueueService.GetFileSystemEvents()
                .Where(r => r.Path == nextEvent.Path && r.CreatedOn >= nextEvent.CreatedOn)
                .ToArray();

            if (nextEvent.ChangeKind == FileSystemEventEnum.Created)
                HandleCreatedEvent(nextEvent, eventsRelated);
            else if (nextEvent.ChangeKind == FileSystemEventEnum.Changed)
                HandleChangedEvent(nextEvent, eventsRelated);
            else if (nextEvent.ChangeKind == FileSystemEventEnum.Deleted)
                HandleDeletedEvent(nextEvent, eventsRelated);

            fileSystemEventQueueService.RemoveFileSystemEventFromQueue(nextEvent);
        }

        private void HandleCreatedEvent(FileSystemEvent eventToHandle, IEnumerable<FileSystemEvent> relatedEvents)
        {
            bool laterDeleted = relatedEvents.Where(r => r.ChangeKind == FileSystemEventEnum.Deleted).Any();

            if (!laterDeleted)
            {
                RemoveFutureChangeEvents(relatedEvents);

                eventService.RaiseCreateEvent(eventToHandle.Path);
            }
            else
                foreach (var fileSystemEvent in relatedEvents)
                    fileSystemEventQueueService.RemoveFileSystemEventFromQueue(fileSystemEvent);
        }

        private void HandleChangedEvent(FileSystemEvent eventToHandle, IEnumerable<FileSystemEvent> relatedEvents)
        {
            Console.WriteLine($"Handling changed event for {eventToHandle.Path}");
            //TODO: Changed -> Later Changed... should pop to singular Change Event
            bool laterDeleted = relatedEvents.Where(r => r.ChangeKind == FileSystemEventEnum.Deleted).Any();

            if (!laterDeleted)
            {
                RemoveFutureChangeEvents(relatedEvents);

                eventService.RaiseUpdateEvent(eventToHandle.Path);
            }
            else
                foreach (var fileSystemEvent in relatedEvents)
                    fileSystemEventQueueService.RemoveFileSystemEventFromQueue(fileSystemEvent);
        }

        private void RemoveFutureChangeEvents(IEnumerable<FileSystemEvent> relatedEvents)
        {
            IEnumerable<FileSystemEvent> changeEvents = relatedEvents.Where(r => r.ChangeKind == FileSystemEventEnum.Changed).ToArray();

            bool laterChanged = changeEvents.Any();

            if (laterChanged)
                foreach (var fileSystemEvent in changeEvents)
                    fileSystemEventQueueService.RemoveFileSystemEventFromQueue(fileSystemEvent);
        }

        private void HandleDeletedEvent(FileSystemEvent eventToHandle, IEnumerable<FileSystemEvent> relatedEvents)
        {
            eventService.RaiseDeleteEvent(eventToHandle.Path);
        }

        public void ListenToCreateEvents(Action<string> handler) => eventService.ListenToCreateEvents(handler);
        public void ListenToUpdateEvents(Action<string> handler) => eventService.ListenToUpdateEvents(handler);
        public void ListenToDeleteEvents(Action<string> handler) => eventService.ListenToDeleteEvents(handler);
    }
}
