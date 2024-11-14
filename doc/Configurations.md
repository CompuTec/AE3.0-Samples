
# Configurations

## Plugin Configuration

Each plugin can include one custom configuration object, manageable at the company level through the AppEngine administration panel. This configuration can later be injected as a service.

```csharp
public class VehicleWebPluginConfig
{
    public bool NotifyUserOnVehicleChanged { get; set; }
    public bool NotifyUserOnVehicleCreated { get; set; }
    public List<string> UsersToNotify { get; set; }
}

/// PluginInfo
public class Info : AEPlugin
{
    public Info()
    {
        PluginCode = "CT.VehOne";
        PluginName = "CT.VehOne Name";
        // Register the configuration to the plugin
        // This configuration is managed in the admin panel at the company level
        SetConfiguration<VehicleWebPluginConfig>();
    }
}
```

## Custom Configuration

You can store additional simple configuration objects using the `IAdditionalConfigurationProvider` service.

## Advanced Configuration

If your configuration requires default value building, UI-side validation, or upgrades when the plugin is updated, implement the `IAdditionalConfiguration` interface:

```csharp
/// <summary>
/// Optional interface for additional configuration capabilities:
/// * Default configuration creation
/// * UI-side configuration validation
/// * Automatic upgrade of saved configuration if version changes
/// </summary>
public interface IAdditionalConfiguration
{
    /// <summary>
    /// Version of the configuration.
    /// </summary>
    Version Version { get; set; }
    
    /// <summary>
    /// Called before configurations are saved.
    /// </summary>
    bool ValidateConfiguration(ICoreConnection connection, out string message);

    /// <summary>
    /// Called when the configuration is upgraded.
    /// Upgrades are detected by comparing the saved version and default configuration version.
    /// </summary>
    bool UpgradeConfiguration(ICoreConnection connection);

    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    IAdditionalConfiguration GetDefaultConfiguration();
}
```

## UI Supported Types

Simple types and collections are supported in AppEngine UI for job and plugin configurations.
