##  Model generator
ct dontent tool that based on the business logic defined in the BL plugin based on the additional annonations will generate:
    * Models for serialization
    * Serializer based on the attributes and logic 
    * CRUD Controllers
    * ODATA Controlers
  best practivce is to set up the generator task in the prj file that will generate required components to AE plugin after BL buld
  

  part of the Busiess logic project file 
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
     <OneUpFolder>$(MSBuildProjectDirectory)\..</OneUpFolder>
```

```xml
    <Target Name="GenerateSerializers" AfterTargets="AfterBuild" Condition="'$(TargetFramework)' == 'net8.0'">
        <Message Importance="High" Text="Generate Models Serializers etc $(ProjectDir) "/>
        <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet tool restore"/>
        <!-- Below Code is used for the generation of the COntrollers models and other files  for the AE plugin
        for more info type dotnet ct gen /h in command line-->
        <Exec Command="dotnet ct gen --Destination $(OneUpFolder)\CT.VehOne\ -l -g scmo -s $(TargetPath) -n CT.VehOne" />
 
    </Target>
```
            **--Destination** path to the forlder whenre the generated Code will be placed
            **-l** log
            **-g** Generate types: s-serialziers c-Crud Controlers o-Odata COntrolers m-Models
            **-n** AE plugin root namespace  
[more about dontnet ct](CTTool.md)

## Udo Bean Required Attributes
To Enable the generaton of the Udo Bean type we must decorate the interfaces with aproptiate attributes

## HEADER Object
### AppEngineUDOBean Attribute  apply to an interface of IUdo
* TableName-  **required** the name of the table eg CT_VO_OVMD 
* ObjectCode- **required** the code of the object eg CT_VO_OVMD
* Ignore- if true then ignored
* ExcludedHttpMethods- you can exclude specific methods from generation eg Patch or post eg:  "POST,DELETE"
* ObjectType- the type of the object 
## Child Object
### AppEngineUDOChildBean Attribute  apply to an interface of IUDOChildBean
* ObjectCode- **required** ObjectCode
* TableName-**required** Table Name
* Parent- **optional** used for tree based relations the name of the Parent child interface ( must contain AppEngineProperty attribute that hold a hey to parent collection )

### Propertie attributes


### AppEngineProperty

```csharp
   public class AppEnginePropertyAttribute : Attribute
    {
        /// <summary>
        /// When setting this attribute to DateTime propeperty will cause the property to be generated as as dateonly Type in model
        /// </summary>
        public bool IsDateOnly = false;
        /// <summary>
        /// When setting this attribute to DateTime propeperty will cause the property to be generated as as timeonly Type in model
        /// </summary>
        public bool IsTimeOnly = false;
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

        /// <summary>
        /// Standard Convention fdor dateaTeim Field is to convert DateTime to DateOnly and When DateTime propertyName Ends With Time then To TimeOnly
        /// this can be disabled by this property
        /// </summary>
        public bool IgnoreConvertionConvention = false;
    }
```
### AppEnginePropertyIgnore
to ignore the property in  the genration 