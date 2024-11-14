
# UI Representation of User-Defined Object (UDO)

In the SAP UI context, we aim to create a model to manage the following aspects of a Form’s events:
- Business logic
- User Data Sources
- Tab handling
- ItemEvents
- Right-click menu events
- Data events

## 1) Basic Setup

Creating a UI model on top of the business logic UDO bean links the SAP UI form with its events, facilitating a framework for:
- **MVVM-like behavior**

For event handling, you can retrieve data from the form as follows:

### Example: Getting UDO from Form
```csharp
public override void OnMatrixLoadBefore(string formUid, ref PItemEvent itemEvent)
```

### Example: Modifying UDO Values
```csharp
private void HandleInvoiceCFL(PItemEvent itemEvent)
{
    DataTable dt = itemEvent.CFL_Event.SelectedObjects;
    LoadFromToAction(itemEvent.CurrentForm, udo =>
    {
        if (dt.Rows.Count == 0)
        {
            // Clear the fields
            udo.U_InvNr = 0;
            udo.SetSellerInvNumber("");
        }
        else
        {
            var tool = _coreConnection.GetService<IVehicleMasterDataUiTools>();
            udo.U_InvNr = dt.GetInt("DocEntry", 0);
            udo.SetSellerInvNumber(tool.GetSellerInvoiceNr(udo.U_InvNr));
        }
    });
}
```

### Example: Filling User Data Sources
```csharp
LoadFromToAction(itemEvent.CurrentForm, udo =>
{
    udo.SetSellerCardName("Filled From Code");
    udo.SetSellerInvNumber("");
});
```

## 2) Setting Up Objects
Define your main UI class to link with the appropriate UDO entity, allowing support for multiple forms per UDO code.

```csharp
public partial class VehicleMasterDataUI : BaseUIBeanLoader<IVehicleMasterData>
```

This links the `VehicleMasterDataUI` UI model with the `IVehicleMasterData` UDO.

### Example: Defining User Data Sources (UDS)
Define UDS to facilitate form interactions, linking data between the form and UDO entity (either the header or a child table).

```csharp
[GenerateUiUdoDefinition(To = typeof(VehicleMasterDataUI), For = "", FormTypeEx = VehicleMasterDataForm.FormTypeEx)]
public partial class VehicleMasterDataUiHeaderUserDataSources
{
    [UDS(UdsType = typeof(string), UdsUniqueId = "CardName")]
    public const string SellerCardName = "SellerCardName";
    
    [UDS(UdsType = typeof(string), UdsUniqueId = "InvDocNum")]
    public const string SellerInvNumber = "SellerInvNumber";
}
```

Using `Computec.SourceGenerator` analyzer, you can generate the necessary extension methods, which can then be utilized in your UI model.

## 3) GetDefinitions Method
The `GetDefinitions` method returns the generated model description. It allows setting up tabs, data sources, matrices, menus, and other UI logic.

```csharp
protected override UserInterfaceUdoDefinitions GetDefinitions()
{
    var definitions = getGeneratedDefinitions();
    definitions
        .ForForm(VehicleMasterDataForm.FormTypeEx)
        .SetTabs(
            TabDef(VehicleMasterDataForm.Controls.DetailsTab, 1),
            TabDef(VehicleMasterDataForm.Controls.OwnersTab, 2, VehicleMasterDataForm.Controls.OnwersMatrix)
        );
    
    return definitions;
}
```

## 4) Loading User Data Sources into the UDO
When a user opens a form with a specific record, you may want to refresh UDS values in the model. Override `FillAllUserDataSource` to manage this, adding any custom logic that should execute each time the data is loaded.

```csharp
protected override void FillAllUserDataSource(IVehicleMasterData bean, Form frm)
{
    var tool = _CoreConnection.GetService<IVehicleMasterDataUiTools>();
    bean.SetSellerCardName(!string.IsNullOrEmpty(bean.U_BuyBy) ? tool.GetSellerName(bean.U_BuyBy) : "");
    bean.SetSellerInvNumber(bean.U_InvNr != 0 ? tool.GetSellerInvoiceNr(bean.U_InvNr) : "");
}
```

## 5) Advanced Scenarios
Additional methods can be overridden as needed:
- **BeforeLoadToForm**: Custom logic before loading data to form
- **AfterLoadToForm**: Custom logic after loading data to form
