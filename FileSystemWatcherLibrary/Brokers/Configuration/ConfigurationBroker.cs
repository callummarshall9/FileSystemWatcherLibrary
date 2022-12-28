using Microsoft.Extensions.Configuration;

namespace FileSystemWatcherLibrary.Brokers.Configuration;

public class ConfigurationBroker : IConfigurationBroker
{
    private readonly IConfiguration configuration;

    public ConfigurationBroker(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GetLocalFolder()
        => configuration.GetSection("FileSystemWatcherLibrary")["LocalFolder"];
}