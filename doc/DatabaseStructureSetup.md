
# General Information

The database structure will be installed if:
- The `PluginInfo` constructor specifies a `SetupDescription` class, which must contain both `Database Version` and `SetupAssembly`.
- A `SetupDefinition` class is implemented, based on `SetupInfo`, containing essential details about the setup version, objects, UFDs, and other components.

The `DatabaseVersion` is a double field that is checked during the update procedure. If this version changes, the setup action performed by the AppEngine installation process will update all objects and apply necessary actions.

---

## 1. User Data Objects

This framework enables the creation of various SAP Business One business object types:
- **Master Data**: Define and manage master data objects.
- **Documents**: Configure different document types.
- **No Objects**: Handle cases without specific objects.
- **No Object AutoIncrement**

All user data object entities must inherit from `UDOManager` and include the `Install` attribute to ensure proper installation.

> **Note**: All child objects defined and used by the CompuTec Core framework contain a `U_LineNum` column, which is a secondary key for UDO child tables. Please avoid using the SAP `LineId` as it may not guarantee integration.

Examples can be found in the demo application `CT.VehOne`.

---

## 2. User Fields in SAP

To add user fields for SAP objects, localize the `PluginSetupInfo` class and add appropriate information to the function:

```csharp
public override List<ICustomField> GetUserDefinedFields(ICoreConnection connection)
{
    var userFields = new List<ICustomField>();
    var VehOneCode = new TableCustomField();
    VehOneCode.SetTableName("OCRD");
    VehOneCode.SetName("VehicleCode");
    VehOneCode.SetDescription("Vehicle Code");
    VehOneCode.SetType(BoFieldTypes.db_Alpha);
    VehOneCode.SetEditSize(50);
    userFields.Add(VehOneCode);
    return userFields;
}
```

---

## 3. Authorizations

You can set up user authorizations to manage access levels for various functionalities in SAP Business One. Authorizations can be defined at the User Data Objects level, where they are automatically assigned to the business entity and form in SAP Business One.

To implement this, create a class that derives from `AuthorizationInfo` and includes the `AuthorizationInstall` attribute. This class enables you to define the complete authorization tree for SAP Business One.

---

## 4. Query Categories

To organize database queries effectively, create and manage query categories by:
1. Creating a class derived from `QueryCategory`.
2. Adding the `Install` attribute.

In the class definition, specify the category name.

---

## 5. Queries

Queries are saved in the Query Manager / SAP Reports. They require a defined query category. To create a query:
1. Create a class derived from `Query` with the `Install` attribute.
2. Specify the following:
   - Query Category
   - Query Description
   - Query String

```csharp
this.QueryCategory = MyQueryCategory.CategoryName;
this.QueryDescription = "Cost Accounting";
this.QueryString = "SELECT * FROM OITM";
```

---

## 6. Formatted Searches

To enhance data entry and automate data retrieval, set up formatted searches. This requires a defined query category and query.

1. Create a class derived from `FormattedSearch`.
2. Add the `Install` attribute.

---

## 7. Alerts

Configure alerts to notify users of important business events or conditions. Alerts require a defined query category and query.

1. Create a class derived from `Alert`.
2. Add the `Install` attribute.

---

## 8. Crystal Layouts

Define crystal document/master data layouts within the relevant `UDOManager` class. This class should describe the entity where the layout is applied.

To set up:
1. Populate the `ReportType` property of the returned `IUDOTable` object.

Example:

```csharp
List<DocumentLayoutInfo> reportList = new List<DocumentLayoutInfo>();
if (_coreConnection.IsSQLConnection())
    reportList.Add(new DocumentLayoutInfo("Bill of Materials (System)", @"Reports\CrystalReports\PF_BOM_SQL_EN.rpt", true, CompuTec.BaseLayer.UI.BoLanguages.ln_English));
else
    reportList.Add(new DocumentLayoutInfo("Bill of Materials (System)", @"Reports\CrystalReports\PF_BOM_HANA_EN.rpt", true, CompuTec.BaseLayer.UI.BoLanguages.ln_English));

UdoTable.ReportType = new ReportTypeUDO("BillOfManufacturingReportType", "", "CT_PF_OBOMCode", reportList);
```

---

## 9. Crystal Reports

Set up Crystal Reports to generate custom reports according to business requirements.

---

## 10. Database Structures

To install the database structure, provide a configuration file (link to example configuration).

- The file should be set as an embedded resource, with the resource path specified in the `SetupInfo` class.
- **Script Node**: Defines the element to install in the database.
  - **Name**: Must match the file name containing the view/procedure/function.
  - **Type**: P = Procedure, F = Function, V = View.
  - **DatabaseType**: A = Install on HANA and SQL systems, S = SQL only, H = HANA only.
  - **ContinueOnError**: Set to `true` or `false` based on whether errors should halt the setup process.

Scripts are installed in the order they appear in the file.

### Directory Structure

- `LoadOrder.xml`
  - `HANA`:
    - Procedures
    - Functions
    - Views
  - `SQL`:
    - Procedures
    - Functions
    - Views

The script body in the file must contain the complete procedure body and declaration, excluding the `CREATE` statement.

---

## 11. Database Upgrade Actions

The setup installation can execute custom code before or after the installation process. This feature is useful for applying new business logic or rules.

To achieve this, create a class derived from `SetupCustomAction` and implement the `public override Result Run()` method for the activity. Add the `ExecuteInstallAction` attribute to execute the class.

1. **Run Once**: Add `[ExecuteInstallAction(true)]`.
2. **Run on Version Upgrade**: `[ExecuteInstallAction(ProcessForceVersionSettings.PF93.PL13)]`.
3. **Run Before Object Installation**: Add `[ExecuteActionBeforeInstallObjects]` to the `ExecuteInstallAction` attribute.

---

## 12. Additional Setup Possibilities

Custom logic can also be called during:
- **Uninstall**
- **Before Install**
- **Installation Started**
- **Installation Error**
- **After Successful Installation**

Override the appropriate methods in the `PluginInfo` class:

```csharp
public override void OnUninstall(ICoreConnection connection) { base.OnUninstall(connection); }
public override void BeforeInstall(ICoreConnection connection) { base.BeforeInstall(connection); }
public override void PluginInstalling(ICoreConnection connection) { base.PluginInstalling(connection); }
public override void OnInstallError(PluginInstalldVersion? current_DB_Number, PluginInstalldVersion final_DB_Number, Result actionResult, Exception ex = null) { base.OnInstallError(current_DB_Number, final_DB_Number, actionResult, ex); }
public override void AfterStructureWasInstalled(PluginInstalldVersion? current_DB_Number, PluginInstalldVersion final_DB_Number, ICoreConnection coreConnection) { base.AfterStructureWasInstalled(current_DB_Number, final_DB_Number, coreConnection); }
```
