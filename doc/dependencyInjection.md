
# IoC in CompuTec.Core2

## Scopes - Basics

The CompuTec.Core2 framework utilizes an IoC (Inversion of Control) approach, allowing it to handle multiple connections to different databases simultaneously. A separate scoped service collection is created for each connection, enabling unique handling and registrations per connection.

### Main Levels of Registration

There are three main levels of registrations available for developers:

1. **Application Level**: Created once at application startup, this primary level scope serves as the base container for all other scopes. Services registered here, like singletons, are accessible across all connections and databases.

2. **Company Level**: Although not a distinct physical scope, this level allows for registering services that are shared across all connections for a specific database.

3. **Connection Level**: The default scope where most registrations occur, specific to each database connection.

---

## Registration Methods

### 1. By Attribute (Default and Most Common Way)

Register a service using an IoC attribute with the following parameters:

- **Scope**: Choose between Application, Company, or Connection.
- **Singleton**: Set to `true` or `false`.
- **ReferenceType**: The type under which the implementation is registered.
- **Named**: Specifies the name of the named implementation if needed.

### 2. By Plugin Information

Using the Autofac IoC framework, registrations can be customized by overriding methods:

- **Application Scope**: Override the `BuildAppContainer` method.
- **Company Scope**: Override the `BuildConnectionContainer` method, ensuring you call the base method.

### 3. On Application Startup

Use this method only if creating a custom application that incorporates the CompuTec Core2 framework.

---

## Creating Instances

### On the Connection

To create an instance of a registered object on the connection level, reference the `ICoreConnection` object and call `GetService<T>()`. Alternatively, use `IServiceProvider` from `Microsoft.Extensions.DependencyInjection.Abstractions` to resolve IoC services.

### Example

```csharp
public interface IVehicleInformationService
{
    string GetVehicleName(string code);
    bool ChangeVehicleNameForVehicle(string code, string name);
    bool AddOwner(string vehicleCode, string ownerName);
}

[Ioc(Scope = IocScope.Connection, ReferenceType = typeof(IVehicleInformationService))]
internal sealed class VehicleInformationService : IVehicleInformationService
{
    private readonly ICoreConnection _coreConnection;
    private readonly ILogger<VehicleInformationService> _logger;

    public VehicleInformationService(ICoreConnection coreConnection, ILogger<VehicleInformationService> logger)
    {
        _coreConnection = coreConnection;
        _logger = logger;
    }
    
    public string GetVehicleName(string code)
    {
        using var measure = _logger.CreateMeasure();
        _logger.LogInformation("Getting vehicle name for code {code}", code);
        return QueryManager.ExecuteSimpleQuery<string>(_coreConnection, "@CT_VO_OVMDA", "Name", "Code", code);
    }

    public bool ChangeVehicleNameForVehicle(string code, string name)
    {
        using var measure = _logger.CreateMeasure();
        var udo = _coreConnection.GetService<IVehicleMasterData>();
        var success = udo.GetByKey(code);
        if (!success)
        {
            _logger.LogWarning("Vehicle with code {code} not found", code);
            return false;
        }
        udo.Name = name;
        return udo.Update() == 0;
    }

    public bool AddOwner(string vehicleCode, string ownerName)
    {
        using var measure = _logger.CreateMeasure();
        var udo = _coreConnection.GetService<IVehicleMasterData>();
        var success = udo.GetByKey(vehicleCode);
        if (!success)
        {
            _logger.LogWarning("Vehicle with code {code} not found", vehicleCode);
            return false;
        }
        udo.Owners.SetCurrentLine(udo.Owners.Count - 1);
        if (!udo.Owners.IsRowFilled())
            udo.Owners.Add();
        udo.Owners.U_OwnerName = ownerName;
        return udo.Update() == 0;
    }
}
```

This example demonstrates defining and implementing an IoC-based service in the CompuTec.Core2 framework, with logging and OpenTelemetry measurement integration.
