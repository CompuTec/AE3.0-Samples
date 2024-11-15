
# Designing SAP UI Forms for Custom Objects

This guide describes the steps for designing SAP UI forms for custom objects, particularly using classes derived from `AbstractFormInitializer` in SAP Business One. It includes instructions on setting up, modifying, and managing form appearances, resizing, and opening custom UDO forms.

## Steps to Create a UI Form

### 1. Create a Class from `AbstractFormInitializer`

If this form is based on UDO (User-Defined Objects) and involves business logic, consider using a generic version of `AbstractFormInitializer`.

### 2. Decorate the Class with the `LoadForm` Attribute

Define the following properties in the `LoadForm` attribute:
- **Form Type**: Specify the form type.
- **MenuUid**: Define the menu UIDs on which this form will load.
- **XML Path**: Path to the XML form definition.

### 3. Customize the Form Appearance in the `InitializeForm` Method

Place your code in the `InitializeForm` method to alter the form’s appearance. Common tasks include setting expand types for combo boxes, rearranging elements, or applying initial settings.

#### Example

```csharp
using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Forms;
using CompuTec.Core2.UI.Translation;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using SAPbouiCOM;

namespace CT.VehOne.UI.UI.Forms.VehicleMasterData;

[LoadForm(FormTypeEx = VehicleMasterDataForm.FormTypeEx, XmlPath = VehicleMasterDataForm.FormXmlDefinitionPath,
    MenuIDS = new string[] { MenuUIds.VehicleMasterDataMenuUid })]
internal sealed partial class VehicleMasterDataForm : AbstractFormInitializer<IVehicleMasterData>
{
    public const string FormTypeEx = "CT_VO_OVMD";
    public const string FormXmlDefinitionPath = "UI\Forms\VehicleMasterData\VehicleMasterDataForm.xml";
    
    public VehicleMasterDataForm(ICoreConnection connection, AppHolder appHolder, ILogger<VehicleMasterDataForm> logger,
        ITranslationServiceUI translationService, IFormDefinitionsCacheService cacheService) : base(connection,
        appHolder, logger, translationService, cacheService)
    {
    }

    public override void InitializeForm(Form form)
    {
        // Called when the form is drawn on the screen.
        form.SetExpandTypeToCombobox(BoExpandType.et_DescriptionOnly, Controls.VehicleTypeCombo);
        form.GetItem<Folder>(Controls.DetailsTab).Select();
    }
}
```

## Designing the Form

### Steps to Design the Form in Practice

1. Add the XML definition as a base definition.
2. Run the application to generate the XML file.
3. Modify the XML file located at `CT.VehOne.UI/bin/Debug/net8.0/UI/Forms/VehicleMasterData/VehicleMasterDataForm.xml`.
4. Reload the form and check the changes.
5. If the design is finalized, copy the modified XML file back to the source directory.

> **Note**: You can also use the screen painter tool for additional customizations.

## Additions to SAP XML Declarations

We have added additional XML declarations that the framework handles. Here’s an example of such an XML configuration:

```xml
<form AutoManaged="1" BorderStyle="0" FormType="CT_PF_OBOMTech" ObjectType="CT_PF_OBOMCode" SupportedModes="13"
      appformnumber="CT_PF_OBOMCode" client_height="590" client_width="890" color="0" default_button="1" height="590"
      left="200" mode="0" pane="8" title="Production Process" top="50" type="0" uid="CT_PF_OBOMTech_" visible="0" width="890">
    <ActiveItem FindDefaultItemUID="7" AddDefaultItemUID="7"/>
    <Resize>
        <Setter TargetElementUid="OBSelOutGr" TargetProperty="Left" ElementUID="rect_rtg4" Property="Right" Ratio="0.5"/>
        <Setter TargetElementUid="OBSelOutGr" TargetProperty="Right" ByChanging="Width" ElementUID="rect_rtg4" Property="Right" Margin ="-5"/>
        <Setter TargetElementUid="OBSelInGrd" TargetProperty="Right" ByChanging="Width" ElementUID="OBSelOutGr" Property="Left" Margin ="-5"/>
        <Setter TargetElementUid="OBSelInGrd" TargetProperty="Top" ElementUID="ClrInBtn" Property="Bottom" Margin ="15"/>
        <Setter TargetElementUid="OBSelOutGr" TargetProperty="Top" ElementUID="ClrInBtn" Property="Bottom" Margin ="15"/>
        <Setter TargetElementUid="OBSelInGrd" TargetProperty="Bottom" ByChanging="Height" ElementUID="rect_rtg4" Property="Bottom" Margin ="-5"/>
        <Setter TargetElementUid="OBSelOutGr" TargetProperty="Bottom" ByChanging="Height" ElementUID="rect_rtg4" Property="Bottom" Margin ="-5"/>
        <Setter TargetElementUid="OBOutGrd" TargetProperty="Left" ElementUID="rect_rtg4" Property="Right" Ratio="0.5"/>
        <Setter TargetElementUid="OBOutGrd" TargetProperty="Right" ByChanging="Width" ElementUID="rect_rtg4" Property="Right" Margin ="-5"/>
        <Setter TargetElementUid="OBInptGrd" TargetProperty="Right" ByChanging="Width" ElementUID="OBOutGrd" Property="Left" Margin ="-5"/>
        <Setter TargetElementUid="AddOutPct" TargetProperty="Left"  ElementUID="OBSelOutGr" Property="Left" Margin ="0"/>
        <Setter TargetElementUid="DelOutPct" TargetProperty="Left"  ElementUID="AddOutPct" Property="Right" Margin ="5"/>
        <Setter TargetElementUid="ClrOutBtn" TargetProperty="Left"  ElementUID="DelOutPct" Property="Right" Margin ="5"/>
    </Resize>
</form>
```

### Resize

> **Note**: Auto resize must be enabled for the framework. Use the `<Resize>` element in the XML rather than manual resizing methods.

- **Setter**: Sets the **TargetProperty** of **TargetElementUid** based on **ElementUID**, **Property**, and **Ratio**.
- **TargetProperty** options:
  - `top`, `left`, `width`, `height`
  - `bottom` and `right` (requires **ByChanging** property)

### ActiveItem

This element sets the active item on the form when the mode changes. For master data documents, set it to the `Code` field in find mode and to the `DocNum` field for documents.

## Opening UDO Forms (Yellow Arrow)

To open UDO forms, use the `IFormOpener` service:

```csharp
_coreConnection.GetService<IFormOpener>().OpenForm<IItemDetails>(code);
```
