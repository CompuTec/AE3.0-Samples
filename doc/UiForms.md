## Designing sap UI Forms for custom objects 
1) Create a class that delivers from the AbstractFormInitializer
if this is UDO based on Business logic use generic verion of this class
2) decorate thic class with LoadForm Attribute and define 
   * Form Type - of the form 
   * MenuUid`s on witch this form will load 
   * xml Path: path to the xml form definition
3) in the initialize method put your code that is changing the formapperiance. most common use is set expand type to combobox some rearrangments or initial settings 
  ```cs
  using CompuTec.Core2.UI.Attributes;
using CompuTec.Core2.UI.Forms;
using CompuTec.Core2.UI.Translation;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using SAPbouiCOM;

namespace CT.VehOne.UI.UI.Forms.VehicleMasterData;


[LoadForm(FormTypeEx = VehicleMasterDataForm.FormTypeEx, XmlPath = VehicleMasterDataForm.FormXmlDefinitionPath,
    MenuIDS = new string[] { MenuUIds.VehicleMasterDataMenuUid })]
internal sealed partial class VehicleMasterDataForm:AbstractFormInitializer<IVehicleMasterData>
{
    
    public const string FormTypeEx = "CT_VO_OVMD";
    public const string FormXmlDefinitionPath = "UI\\Forms\\VehicleMasterData\\VehicleMasterDataForm.xml";
    
    public VehicleMasterDataForm(ICoreConnection connection, AppHolder appHolder, ILogger<VehicleMasterDataForm> logger,
        ITranslationServiceUI translationService, IFormDefinitionsCacheService cacheService) : base(connection,
        appHolder, logger, translationService, cacheService)
    {
    }

    public override void InitializeForm(Form form)
    {
        //Its called when the form is dreawn on the screen ( use this method instead of On Form Load Event for custom objects)
        //Send Expand Type
         form.SetExpandTypeToCombobox(BoExpandType.et_DescriptionOnly,Controls.VehicleTypeCombo);
         //Select Default Folder
         form.GetItem<Folder>(Controls.DetailsTab).Select();
    }
}

  ```
### Designing the form in practive 
1) add the xml definition as a base definition
2) run the application
3) modify the xml from the output directory **CT.VehOne.UI/bin/Debug/net8.0/UI/Forms/VehicleMasterData/VehicleMasterDataForm.xml**
4) open form again and check the result 
5) if all is good copy file from output to the source 
**you can also use screen painter
## Additions to the sap xml Declaration
we have added some additional xml deflarations to the xml witch will be handled by framework 
here is an xml example
```xml
  <form AutoManaged="1" BorderStyle="0" FormType="CT_PF_OBOMTech" ObjectType="CT_PF_OBOMCode" SupportedModes="13" appformnumber="CT_PF_OBOMCode" client_height="590" client_width="890" color="0" default_button="1" height="590" left="200" mode="0" pane="8" title="Production Process" top="50" type="0" uid="CT_PF_OBOMTech_" visible="0" width="890">
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
```
#### Resize 
(!) all our framework requires sap option Auto resize to be enabled 
instead of manually resize metchodod please use the resize element in the xml
**Setter**: declring this we will set the **targetproperty** of **TargetElementUid** based on the **ElementUID** and **property** and *ratio*
**TargetProperty** availble options:
    * top
    * left 
    * width
    * height
    * bottom when using this **ByChanging** property is required
    * right when using this **ByChanging** property is required
  
#### ActiveItem
this element will set the active item on the form when the form changes the mode. 
its a good practice that on master data documents when in find mode active item is set to Code field, and on document DocNum Field.
## Opening Forms

For Opening UDO Forms Please use `IFormOpener` service 
```csharp
	_coreConnection.GetService<IFormOpener>().OpenForm<IItemDetails>(code);
```