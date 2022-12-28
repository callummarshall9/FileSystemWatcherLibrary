using System;
using EventLibrary;
using FileSystemWatcherLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {
        services.AddSingleton<IEventHub, EventHub>();
        services.AddFileSystemEventWatcherServices();
    })
    .Build();

MonitorFileSystem(host.Services);

await host.RunAsync();

static void MonitorFileSystem(IServiceProvider services)
{
    IFileSystemWatcherLibrary fileSystemWatcherLibrary = services.GetService<IFileSystemWatcherLibrary>();

    fileSystemWatcherLibrary.ListenToCreateEvents((path) => {
        Console.WriteLine($"Received create event for {path}");
    });
    fileSystemWatcherLibrary.ListenToUpdateEvents((path) => Console.WriteLine($"Received update event for {path}"));
    fileSystemWatcherLibrary.ListenToDeleteEvents((path) => Console.WriteLine($"Received delete event for {path}"));
}