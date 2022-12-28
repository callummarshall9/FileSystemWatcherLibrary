using System;

namespace FileSystemWatcherLibrary.Models
{
    public class FileSystemEvent
    {
        public string Path { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public FileSystemEventEnum ChangeKind { get; set; }
    }
}
