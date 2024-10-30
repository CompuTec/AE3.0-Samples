## UI representation of User Defined Object 
From the sap UI perspective we must create a model that will be used on Form`s events to manage:
* Business logic 
* User Data Sources 
* Form`s tabs handling 
1) THE BASIC 
   creating a UI model on top of the business logic udo bean will link the sap UI form and its events it will allow the framework to manage 
   - MVVM like behaviour 
 in the event hanlder you can get the data from form by:
      - Get Udo from form 
```cs
    public override void OnMatrixLoadBefore(string formUid, ref PItemEvent itemEvent)

```
Modify the udo values 
  
```cs
  private void HandleInvoiceCFL(PItemEvent itemEvent)
    {
        DataTable dt = itemEvent.CFL_Event.SelectedObjects;
        LoadFromToAction(itemEvent.CurrentForm, udo =>
        {
            if (dt.Rows.Count == 0)
            {
                //Clear the fields
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
Fill the user data sources
```cs
    LoadFromToAction(itemEvent.CurrentForm, udo =>
        {
                udo.SetSellerCardName("Filled From Code");
                udo.SetSellerInvNumber("");
        });
```
Data Events autoamtically execute bussiens logic validations
Collection support Matix settings etc.
  
## Set up the objects 
   ```cs 
   public partial class VehicleMasterDataUI : BaseUIBeanLoader<IVehicleMasterData>
    ```
   here we link a UI  Model **VehicleMasterDataUI** with  **IVehicleMasterData** UDO
* Each udo can support multiple Forms that are binded with specific udo Code
* User data sources 
  You can define user data shources that are used on the form and add them as extensions methods as get and set for setting or getting the UDS value from form.
To add UDS information to the UI model add class that descripe:
* what form is related to those uds 
* What udo entity is corresponded to this entity it can be a header or child table 
  example
```cs 
        [GenerateUiUdoDefinition(To = typeof(VehicleMasterDataUI), For = "",
            FormTypeEx = VehicleMasterDataForm.FormTypeEx)]
        public partial class VehicleMasterDataUiHeaderUserDataSources
        {
            /// <summary>
            /// Definition of a User data Sources of the Form  and user defined object
            /// this definitipon will generate some helpers method that then can be used to set up the UDS values to related object 
            /// </summary>
            [UDS(UdsType = typeof(string), UdsUniqueId = "CardName")]
            public const string SellerCardName = "SellerCardName";
            
            [UDS(UdsType = typeof(string), UdsUniqueId = "InvDocNum")]
            public const string SellerInvNumber = "SellerInvNumber";

        }
    
        [GenerateUiUdoDefinition(To = typeof(VehicleMasterDataUI), For = PrevOwnersTableName,
            FormTypeEx = VehicleMasterDataForm.FormTypeEx, Matrix = VehicleMasterDataForm.Controls.OnwersMatrix)]
        public partial   class VehicleMasterDataUiPreviousOwnersUserDataSources
        {
            // [UDS(UdsType = typeof(string), UdsUniqueId = "WrdNameDs")]
            // public const string DS_WordName = "DS_WordName";
        }
```
Computec.SourceGenerator analyzer will generate aprorpiate extensions and method in your UI model 
now finally we must return generated Model Desctiption usin **GetDefinitions** method
```cs
 protected override UserInterfaceUdoDefinitions GetDefinitions()
        {
            //in this function we are creating the description of a form it can support multiple form types for the same udo 
            //it handles:
            // - the tabs
            //User data spources
            //Matrixes - settoings add menus and all other logic
            var definitinions = getGeneratedDefinitions();
            definitinions
            .ForForm( VehicleMasterDataForm.FormTypeEx)
            .SetTabs(
                TabDef(VehicleMasterDataForm.Controls.DetailsTab, 1),
                TabDef(VehicleMasterDataForm.Controls.OwnersTab, 2,VehicleMasterDataForm.Controls.OnwersMatrix)
                );
            
            return  definitinions;
        }
```
## Loading User Data sources to the UDO
User opens a form with specific record we want to also refresh the user datasources values in the model 
to do this we must override the **FillAllUserDataSource**
this method can be also used to add additional logic on to the form each time when user load a data to the form ( yellow arrow or navigate between objects)
```cs
     protected override void FillAllUserDataSource(IVehicleMasterData bean, Form frm)
        {
            var tool = _CoreConnection.GetService<IVehicleMasterDataUiTools>();
            if(!string.IsNullOrEmpty(bean.U_BuyBy))
                bean.SetSellerCardName(tool.GetSellerName(bean.U_BuyBy));
            else
                bean.SetSellerCardName("");
            if (bean.U_InvNr != 0)
                bean.SetSellerInvNumber(tool.GetSellerInvoiceNr(bean.U_InvNr));
            else
                bean.SetSellerInvNumber("");
        }
```
##  Advanced Scenarios
there are seveal methods that can be overrided when nessesery such as 
   **BeforeLoadToForm** you can add your logic that will be called before Load To Form
   **AfterLoadToForm** you can add your logic that will be called after Load To Form 
   
