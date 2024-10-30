# Implementing user defined objects 
- prepare the  [database Structure](DatabaseStructureSetup.md)
## class inheritiance table
| SAP UDO Types | BaseClass | Base Interface | description |
|-------------------------|------------------|----------------------|-----------------------------------|
| MasterData          | MasterDataBean  | IMasterDataBean | Code can be generated automatiaclly (default) or manually bu user (   AutoCodeGeneration = false; inconstructor)    |
|Master Data lines| MasterDataChildBean<T> where T:IMasterDataChildBean|IMasterDataChildBean| lines in master data object|
|Document |DocumentBean | IDocumentBean | Document header declaration |
|Document Lines|DocumentChildBean<T>  where T:IDocumentChildBean| IDocumentChildBean |Document Lines declaration |
|No Object | NoObjectBean |INoObjectBean | for simple entry with code and name no child objects are supported|
|No Object - Autoincrement | AutoIncrementBean | IAutoIncrementBean | for simple entry with ID no child objects are supported |



## The Basics
create an interface that is public and avaible for other part of the application this interface will be registered in dpenedncy injection container and rtesolved on demand by the interface. This interface must contain all fields and child collection.
The class that implement your interface should be **internal sealed parial**. In the class you putt all your BUsiness logic 
### Fields 
1) Getter and setter: please set the setter acoording to the business needs- if you want to block user for setting the specific value add only getter to the interface 
```cs
    int U_RedOnlyField{get;}
    int U_IntegerField{get;set;}
```
2) Supported Field Types 

| .net type| sap types | afailble Attributes|
|---|---|---|
|int| numeric|[DefaultValue]|
|double| price quantity,weihght,amout,sum, etc...|[DefaultValue]|
|string| alpha , memo |[DefaultValue]|
|dateTime| date ||
|dateTime| Time |mandatory [TimeType]|
|enumerators| alphanumeric |[DefaultValue]|
|TimeSpan| numeric |mandatory [TimeSpanType] stored as secounds in the db|

* for enums please decoreate enum declaration with EnumType attribute 
```cs
[EnumType(new int[] { 1, 2,3,4,5 }, new string[] { "C", "B" ,"T","M","O"}, 1)]
public enum VechicleType
{
    Car = 1,
    Bus = 2,
    Truck = 3,
    Motorcycle = 4,
    Other = 5
}
```

### Child Collections 
[!] in all UDO collections the secondary key is U_LineNum instead of the sap standard LineId 

1) Specify the interface and for a child class this interface must inherit from interface based on the Udo Type for document IDocumentChildBean for master data IMasterDataChildBean
*  **Linq support for collection** to enable linq Support please use generic base interface :
```cs
public interface IVechickePreviusOwners : IMasterDataChildBean<IVechickePreviusOwners>
```
1) Specify the class implementation 
```cs 
internal sealed  partial class VechickePreviusOwners : MasterDataChildBean<IVechickePreviusOwners>,
    IVechickePreviusOwners
{
```
* generator can generate all the interface simple properties for you by adding  attribyte to the class
```cs 
[GenerateUdoBeanProperties(typeof(IVechickePreviusOwners))]
``` 
2) delcare the child in the header object 
* in interface add property 

```cs
      public IVechickePreviusOwners Owners { get; }
```

* in class define
     
```cs
    /// table oname of the child collection
    public const string __ownerLines = VehiclePreviousOwnersTable.TableNaame;
    public VechicleMasterDataBean(ICoreConnection coreConnection, ILogger<VechicleMasterDataBean> logger,ITranslationService translationService) : base(coreConnection, logger)
    {
    ...
        AddChildDefinition<VechickePreviusOwners>(__ownerLines);
    }

        public IVechickePreviusOwners Owners
    {
        get=>GetChild<IVechickePreviusOwners>(__ownerLines);
    }
```

### Events
in the UDO and Child Udo you can use several events that iwll be triggered when the value is changing
```cs
        this.RowAdded += rowAdded;
        this.RowDeleted += rowDeeted;
        this.ValueChanging += changing;
        this.ValueChanged += changed;
    }

    private void rowDeeted(UdoEventArguments e)
    {
        _logger.LogDebug("Row deleted {row}", e.ObjectName);
    }

    private void rowAdded(UdoEventArguments e)
    {
        _logger.LogDebug("Row added {row}", e.ObjectName);
    }

    private void changed(object sender, PropertyChangedEventArgs eventargs)
    {
        _logger.LogDebug("Value changed {property}", eventargs.PropertyName);
    }

    private void changing(object sender, PropertyChangingEventArgs eventargs, out bool canel)
    {
        canel = false;
        _logger.LogDebug("Value changing {property}", eventargs.PropertyName);
    }
```
### Validations
in the class you can implement the Validations that are execute on the Before Add,Update,Delete
the validation if occures must throw exception with apropriate message 
```cs
 protected override bool BeforeUpdate()
    {
        Validate();
        return true;
    }

    private void Validate()
    {
        
        ValidateViN();
        ValidateRegistrationNumber();
        ValidateModelAndBrand();
        ValiddateDuplicatedOwnerNames();
    }
```

### Formulations 
you can add forumalations to the Object. to do this you must define the formulation rules such as
* add **IFormulatedBean** interface to your object interface declaration
* implement **GetFormulaDefinition()** method that will returns the formulation definition it must descripe what field in header or collection contains the formula evaluation declaration and where to put the result of the formula.
For Collection you can specufy the columnName where the sequence is filled by default is U_Sequencebut you can change it. please notice that you should  implement the logic that can will increment the sequence field 
```cs
   public UdoFormulaDefinition GetFormulaDefinition()
        {
          return   new UdoForomulaDefinitionBuilder()
                .AddDefinitionForCollection("Lines", __LinesTableName, nameof(TestMasterLines.U_Formula),
                    nameof(TestMasterLines.U_Result))
                .Build();
        }
```
* implement **RecalcFormulas** method that will call the recalculation
```cs
public void RecalcFormulas(bool recalculateAll = true)
	{
    	var formulator = _coreConnection.GetService<IFormulaService>().GetFormulator(this);
		formulator.CalulateFormula(recalculateAll);
	}
```

* implement **RecalcRequired** method that will check if calculation is required

```cs
public bool RecalcRequired()
	{
    	var formulator = _coreConnection.GetService<IFormulaService>().GetFormulator(this);
        return formulator.IsCalulateFormulaRequired();
    }

```
* on Before add and Before Update please add following line to trigger the calculations before adding or updating the object 
```cs
  if (RecalcRequired())
	RecalcFormulas();
```

## Additional Attributes on UDO

#### [Bean] Required
apply on the Class of UDO: required i register a class as in depInjection
* UdoCode: Udo Code
* TableName: Table Name 
* Description: Descriptopn
* LicenseType: to be used in the licensing mechanism future request 
* LicenseSubType:to be used in the licensing mechanism future request 
* LicenseValue:to be used in the licensing mechanism future request 
* LicenseValue2:to be used in the licensing mechanism future request 
#### [DefaultValue] optional on the property 

#### [EnumType] required on the enum declaration 
it is required when you want to export the enum as a property type 
it binds enum values to the database string representation
#### [EnumType] optonal on the child property declaration 
Thius classis sed to automaticly implement Key Value from Header to Child Table 
Used when child Table Has a key that is synchronized with header Value 
Echa change of Header Value Changes its values in Childs 
and it is added each time in childs while adding new row
We put this on property in child
#### [TimeType] optional on the property (DateTime) and sap type of time 
 Allows to add Date Time filed on UDO objects but holds Time Only
 #### [TimeSpanType] optional on the property (TimeSpan) and sap type of long numebr 
 it holds total secounds in database and expose as timespan property in udo 

### AppEngine 
#### [AppEnginePropertyReadOnly] 
appply on property: AE generator will make this property readonly in the web entity Model

#### [AppEngineUDOBeanAttribute]  
apply on UdoBean interface: it is required to expose udo to web entity model
 * Ignore = false;

* ObjectType: object type ;

* ObjectCode: object Code

* TableName:Table Name 
* ExcludedHttpMethods: 

example

```cs
[AppEngineUDOBean(Ignore = false, ObjectType = "ManufacturingOrder", TableName = ManufacturingOrder.TABLENAME, ObjectCode = ManufacturingOrder.OBJECTCODE, ExcludedHttpMethods = "DELETE")]
```

#### [AppEngineUDOChildBean]  
apply on childBean interface: it is required to expose collection to web entity model

* **Parent**  optional for multilevel child definitions name of a inteface that is parent in hirerarhy:

*  **ObjectCode**: required code of the collection

* **TableName**: optional, table name of the collection 
#### [AppEngineProperty]
apply on peoprty on UDOheader of child interface: This property is required when:
* you want to set up the custom order of fields to be seialized from model to udo and vice versa
* you want to define multilevel child hierarhy 
Possibilietes:
```cs
  
        /// <summary>
        /// The Priority of a property the lower priority the higher the sooner this property will be filled in UDO from model
        /// </summary>
        public int Priority;
        /// <summary>
        /// The name of a parent property that this property is a child of
        /// </summary>
        public string ParentKey;

        public bool Required;

        public bool Autogenerate;
        
        
        /// <summary>
        /// If Patch with  SPecific Prperty is present system will automatically clear the mentioned Collections Names
        /// Set this to Lines collection when Header filed is controlling the content of the lines for example when setting up the BOMCode system will change all Raw Materials  
        /// </summary>
        public string[] ClearChilds;


```
#### [AppEnginePropertyIgnore] 
when  property is decorated will be ommited in the web entity model 

 
