
# Implementing User-Defined Objects

To begin implementing user-defined objects (UDOs), first prepare the [Database Structure](DatabaseStructureSetup.md). This guide covers UDO types, class definitions, fields, collections, events, validations, formulas, and attributes.

## Class Inheritance Table

| SAP UDO Types           | Base Class                | Base Interface          | Description                                                                                       |
|-------------------------|---------------------------|--------------------------|---------------------------------------------------------------------------------------------------|
| MasterData              | MasterDataBean            | IMasterDataBean          | Code can be generated automatically or manually by setting `AutoCodeGeneration = false`.          |
| Master Data Lines       | MasterDataChildBean<T>    | IMasterDataChildBean     | Lines in master data object.                                                                      |
| Document                | DocumentBean              | IDocumentBean            | Document header declaration.                                                                      |
| Document Lines          | DocumentChildBean<T>      | IDocumentChildBean       | Document lines declaration.                                                                       |
| No Object               | NoObjectBean              | INoObjectBean            | Simple entry with code and name; no child objects supported.                                      |
| No Object - Autoincrement | AutoIncrementBean     | IAutoIncrementBean       | Simple entry with ID; no child objects supported.                                                 |

## Basic Implementation

1. **Create an Interface**: Define a public interface for the UDO, registered in dependency injection. The interface contains all fields and child collections.
2. **Implement the Class**: The class should be `internal sealed partial` and implement business logic.

### Fields

1. **Getter and Setter**: Use the setter according to business needs. To restrict certain fields, only add a getter.

    ```csharp
    int U_RedOnlyField { get; }
    int U_IntegerField { get; set; }
    ```

2. **Supported Field Types**:

    | .NET Type       | SAP Type   | Available Attributes            |
    |-----------------|------------|---------------------------------|
    | `int`           | `numeric`  | `[DefaultValue]`                |
    | `double`        | `price`    | `[DefaultValue]`                |
    | `string`        | `alpha`    | `[DefaultValue]`                |
    | `DateTime`      | `date`     |                                 |
    | `DateTime`      | `time`     | `[TimeType]`                    |
    | `enum`          | `alpha`    | `[DefaultValue]`, `[EnumType]`  |
    | `TimeSpan`      | `numeric`  | `[TimeSpanType]`                |

3. **Enum Definition**:

    ```csharp
    [EnumType(new int[] { 1, 2, 3, 4, 5 }, new string[] { "C", "B", "T", "M", "O" }, 1)]
    public enum VehicleType
    {
        Car = 1,
        Bus = 2,
        Truck = 3,
        Motorcycle = 4,
        Other = 5
    }
    ```

### Child Collections

To enable LINQ support for collections, use a generic base interface:

```csharp
public interface IVehiclePreviousOwners : IMasterDataChildBean<IVehiclePreviousOwners> { }
```

1. **Specify Class Implementation**:

    ```csharp
    internal sealed partial class VehiclePreviousOwners : MasterDataChildBean<IVehiclePreviousOwners>, IVehiclePreviousOwners
    {
        [GenerateUdoBeanProperties(typeof(IVehiclePreviousOwners))]
    }
    ```

2. **Declare Child in Header Object**:

    ```csharp
    public IVehiclePreviousOwners Owners { get; }
    ```

### Events

UDO and child UDO classes can trigger events:

```csharp
this.RowAdded += rowAdded;
this.RowDeleted += rowDeleted;
this.ValueChanging += changing;
this.ValueChanged += changed;

private void rowDeleted(UdoEventArguments e) { }
private void rowAdded(UdoEventArguments e) { }
private void changed(object sender, PropertyChangedEventArgs e) { }
private void changing(object sender, PropertyChangingEventArgs e, out bool cancel) { cancel = false; }
```

### Validations

Implement validations in `BeforeAdd`, `BeforeUpdate`, and `BeforeDelete` methods:

```csharp
protected override bool BeforeUpdate()
{
    Validate();
    return true;
}

private void Validate()
{
    ValidateVIN();
    ValidateRegistrationNumber();
}
```

### Formulations

To add formula definitions:

1. Add `IFormulatedBean` to the interface.
2. Implement `GetFormulaDefinition()` and define formulas.

    ```csharp
    public UdoFormulaDefinition GetFormulaDefinition()
    {
        return new UdoFormulaDefinitionBuilder()
            .AddDefinitionForCollection("Lines", __LinesTableName, nameof(TestMasterLines.U_Formula), nameof(TestMasterLines.U_Result))
            .Build();
    }
    ```

3. Implement `RecalcFormulas()` to trigger calculations.

    ```csharp
    public void RecalcFormulas(bool recalculateAll = true) { ... }
    ```

## Additional Attributes on UDO

### Bean Attribute

Required on UDO class:

- **UdoCode**: Code of the UDO.
- **TableName**: Table name.
- **Description**: Description of the UDO.

### DefaultValue Attribute

Optional on the property to set a default value.

### EnumType Attribute

Required on enum declarations for binding enum values to database representation.

### AppEngine Attributes

#### AppEnginePropertyReadOnly

Marks a property as read-only in the web entity model.

#### AppEngineUDOBeanAttribute

Exposes UDO to the web entity model.

Example:

```csharp
[AppEngineUDOBean(Ignore = false, ObjectType = "ManufacturingOrder", TableName = ManufacturingOrder.TABLENAME, ObjectCode = ManufacturingOrder.OBJECTCODE)]
```

#### AppEngineUDOChildBean

Applied on child beans to expose the collection to the web entity model.

#### AppEngineProperty

Defines custom order of fields to serialize between model and UDO.

#### AppEnginePropertyIgnore

When applied, omits the property in the web entity model.
