using FileSystemWatcherLibrary.Brokers;
using FileSystemWatcherLibrary.Brokers.Configuration;
using FileSystemWatcherLibrary.Brokers.Events;
using FileSystemWatcherLibrary.Brokers.Queues;
using FileSystemWatcherLibrary.Services.Foundation;
using FileSystemWatcherLibrary.Services.Orchestration;
using Microsoft.Extensions.DependencyInjection;

namespace FileSystemWatcherLibrary
{
	public static class ServiceProviderExtensions
	{
		public static void AddFileSystemEventWatcherServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IConfigurationBroker, ConfigurationBroker>();

			serviceCollection.AddTransient<IEventLibraryBroker, EventLibraryBroker>();
			serviceCollection.AddTransient<IFileSystemEventBroker, FileSystemEventBroker>();
			serviceCollection.AddTransient<ITimerEventBroker, TimerEventBroker>();

			serviceCollection.AddTransient<IFileSystemEventQueueBroker, FileSystemEventQueueBroker>();

			serviceCollection.AddTransient<IEventService, EventService>();
			serviceCollection.AddTransient<IFileSystemEventQueueService, FileSystemEventQueueService>();
			serviceCollection.AddTransient<IFileSystemEventService, FileSystemEventService>();
			serviceCollection.AddTransient<ITimerEventService, TimerEventService>();

			serviceCollection.AddTransient<IFileSystemEventOrchestrationService, FileSystemEventOrchestrationService>();

			serviceCollection.AddTransient<IFileSystemWatcherLibrary, FileSystemWatcherLibrary>();
		}
	}
}

