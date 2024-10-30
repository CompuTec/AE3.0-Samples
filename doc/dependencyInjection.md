
# IoC in CompuTec.Core2

## Scopes - Basics

The entire framework uses an IoC (Inversion of Control) approach, allowing it to handle multiple connections to different databases simultaneously. A separate scope service collection is created for each connection, and as connections may vary by database (due to different enabled plugins), implementation and registrations can be handled differently per connection.

There are three main levels of registrations available for developers:

1. **Application Level**: This primary level scope is created once when the application starts. This container acts as the base container for all other scopes, meaning that services registered at the application level are available to all connections across databases. For instance, a singleton service registered here, like a cache, will be accessible for each company and each connection.

2. **Company Level**: While not a physical scope (own IoC container), this level allows registering a singleton service on the company level, shared across connections for each database.

3. **Connection Level**: The default scope where most registrations occur.

---

## Registration

### 1. By Attribute (Default and Most Common Way)

You can register a service using an IoC attribute with the following parameters:

- **Scope**: Choose between Application, Company, or Connection as described above.
- **Singleton**: Set to true or false.
- **ReferenceType**: The type with which the annotated implementation will be registered.
- **Named**: Specify the name of the named implementation, if required.

### 2. By Plugin Information

This version of AppEngine uses the Autofac IoC framework, so all registrations follow this approach. To register services in the application scope, override the `BuildAppContainer` method. For company scope registration, override the `BuildConnectionContainer` method, ensuring you call the base method in each.

### 3. On Application Startup

This mode is used only if creating a custom application that utilizes the CompuTec Core2 framework.

---

## Creating Instances

### On the Connection

To create an instance of a registered object on the connection level, reference the `ICoreConnection` object and call the `GetService<T>()` method. Alternatively, use the `IServiceProvider` class from `Microsoft.Extensions.DependencyInjection.Abstractions` to resolve IoC services.

### Example

```csharp
public interface IVehicleInformationService
{
    string GetVehicleName(string code);
    bool ChangeVehicleNameForVehicle(string code, string name);
    bool AddOwner(string vehicleCode, string ownerName);
}

[Ioc(Scope= IocScope.Connection, ReferenceType = typeof(IVehicleInformationService))]
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

This example illustrates how to define and implement an IoC-based service in the CompuTec.Core2 framework, ensuring proper logging and OpenTelemetry measurement creation during method execution.
