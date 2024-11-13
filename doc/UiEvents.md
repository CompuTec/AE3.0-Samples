## SAP B1 Events
we can add events to all sap forms
Standard Sap Events:
* Item Event -**ApplicationItemEvent**
* RigntClickEvent- **ApplicationRightClickEvent**
* Data Event-  **ApplicationDataEvent**
* Menu Event- **ApplicationContextMenuEvent**
 Each of above event types to iimplement must derive from apropriate base class and has **[EnableEvent]** Attribute.
 ### Enable event Attribute 
 this attribute if used then class will be invoked when sap event occures.
If You add this attribute withut **FormTypeAttribute** it will be invoked each time the event occures.and from all forms. in this case please implement the methd 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
For better performance we strongly recoment to fill up this properrty

### Bubble Event Attribute 
to cancel the before event please call the 
```cs
setBubbleEvent(true);
```
### GEneric version of events;
If you are implementing your custom User Define object with UDo Bean Business logic implementation please use generic verrion of the base class 
it will add all the helpers method to load data from and to UDO BEAN to simplify the implementation.

in this section we are focusing on the standard sap forms and ustom forms that are not user defined objects
#### Item Event
1) create a class that iherits from **ApplicationItemEvent** and add attribute 
2) Add **[EnableEvent]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
4) Override the method that you want to implement for example 
   * OnItemPressedBefore
   * OnItemPressedAfter
   * OnChooseFromListBefore
   * OnChooseFromListAfter
   * .....
  
#### Data Event
1) create a class that iherits from **ApplicationDataEvent** and add attribute 
2) Add **[EnableEvent]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
4) Override the method that you want to implement for example 
   * OnFormDataAddBefore
   * OnFormDataAddAfter
   * OnFormDataLoadBefore
   * OnFormDataLoadAfter
   * .....
  
#### RigntClickEvent
1) create a class that iherits from **ApplicationRightClickEvent** and add attribute 
2) Add **[EnableEvent]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
4) Override the method that you want to implement for example 
   * OnRightClickBefore
   * OnRightClickAfter

#### MenuEvent
1) create a class that iherits from **ApplicationContextMenuEvent** and add attribute 
2) Add **[EnableEvent]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
4) Override the method that you want to implement for example 
   * OnMenuClickBefore
   * OnMenuClickAfter

### LoadReportEvent
1) create a class that iherits from **ApplicationLayoutEvent** and add attribute 
2) Add **[EnableEvent]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx =="134";
    }
```
4) Override the method that you want to implement for example 
   * OnBeforeLayoutKeyPressed
   * OnAfterLayoutKeyPressed

### Custom UDO Object Forms
this part is related to the forms that are binded to the Udo Bean objects
#### Item Event
1) create a class that iherits from **ApplicationItemEvent\<T>** where T:IUDOBean 
2) Add **[EnableEvent(FormTypeEx)]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx ==FormTypeEx;
    }
```
1) Override the method that you want to implement for example 
   * OnItemPressedBefore
   * OnItemPressedAfter
   * OnChooseFromListBefore
   * OnChooseFromListAfter
   * .....
2) you have build in mechanism to get the Udo direcly in event or set the value [More info](BusinessLogicUiModel.md)
#### Data Event
1) create a class that iherits from **ApplicationDataEvent\<T>** where T:IUDOBean 
2) Add **[EnableEvent(FormTypeEx)]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx ==FormTypeEx;
    }
```
4) The business logic will be execute out of the box 
5) to add custom validation exception logic override:
   - HandleAddException method
   - HandleUpateException method

  
#### RigntClickEvent
1) create a class that iherits from **ApplicationRightClickEvent\<T>** where T:IUDOBean 
2) Add **[EnableEvent(FormTypeEx)]** attribute 
3) Override the **HandleEvent** method that indicates weather this class is handling specific form type 
```cs
public override bool HandleEvent(ref PItemEvent itemEvent)
    {
        return itemEvent.FormTypeEx ==FormTypeEx;
    }
```
1) The business logic will be execute out of the box such as  adding/removing lines to collection. 
2) Add custom context Menues follow the exapmples:
   TODO:
#### MenuEvent
1) create a class that iherits from **ApplicationContextMenuEvent\<T>** where T:IUDOBean 
2) Add **[EnableEvent(FormTypeEx)]** attribute 
 
```
1) The business logic will be execute out of the box such as  adding/removing lines to collection. 
2) Handling Custom Menu events:
   TODO:
### LoadReportEvent
