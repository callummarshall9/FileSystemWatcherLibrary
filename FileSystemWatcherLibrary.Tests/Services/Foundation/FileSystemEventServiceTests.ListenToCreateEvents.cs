using Moq;
using System;
using Xunit;

namespace FileSystemWatcherLibrary.Tests.Services.Foundation
{
    public partial class FileSystemEventServiceTests
    {
        [Fact]
        public void ListenToCreateEventsWorksAsExpected()
        {
            //given
            Action<string> handler = (string path) => { };

            //when

            //then
        }
    }
}
