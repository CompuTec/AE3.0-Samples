## SAP B1 Events
we can add events to all sap forms but for clear documentation we divide this section on thwo main areas 
in general you can split event hanlding to seperate classes that are grouped by Form Type to have the code much more organized
### Bubble Event 
to cancel the before event please call the 
```cs
setBubbleEvent(true);
```
### Sap or custom forms
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
4) The business logic will be execute out of the box such as can delete or adding lines to collection and duplicate, 
5) to add custom Menues follow the exapmples:
   TODO:
#### MenuEvent
### LoadReportEvent
