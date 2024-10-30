To provide guidance on how to specify values for the UI entities in JSON and understand the validation logic, let's break down each entity and detail the fields, acceptable values, and validation rules. This information is crucial for correctly preparing JSON configurations.

### General Structure

Each UI entity (`UiDescription`, `UICondition`, `UIParameter`) should be represented as a JSON object. The fields within each object represent properties of that entity, and the values assigned to those fields must adhere to the rules outlined below.

### UiDescription

This is the main entity that describes a UI element, such as a form, context menu, or analysis. The JSON for a `UiDescription` might look like this:

```json
{
  "Type": "Form",
  "FormType": "frm_sales_order",
  "MenuUid": "unique_sales_order",
  "Url": "https://example.com",
  "MenuCaption": "Sales Order",
  "MenuCationTranslationId": "Menu_Translation_Id",
  "TitleTranslationId": "Title_Translation_Id",
  "Title": "Sales Order",
  "Condition": { ... },
  "Parameters": [ ... ]
}
```

- **Type** (required): Can be `"Form"`, `"ContextMenu"`, or `"Analytics"`. Determines the type of UI element.
- **FormType**: Unique identifier for forms. Required if `Type` is `"ContextMenu"`.
- **MenuUid** (required): A unique identifier for the UI element.
- **FatherMenuUid**: Required if `Type` is `"Form"`. It specifies the parent element.
- **Url** (required): The URL associated with the UI element.
- **InternalUrl**: The internal URL associated with the Plugin ( hosted in AppEngine plugin).
- **MenuCaption**: Required for `"ContextMenu"` and `"Form"` types. It's the text displayed in the menu.
- **Title**: The title of the UI element. Required for all types.
- **Condition**: An object that specifies conditions for the UI element's visibility. It's required for `"ContextMenu"`.
- **Parameters**: An array of `UIParameter` objects that define parameters used by the UI element.
- **MenuCationTranslationId**: Translation ID for the `MenuCaption`.
- **TitleTranslationId**: Translation ID for the `Title`.
### UICondition

Specifies conditions under which a UI element is visible or available. A `UICondition` JSON might look like:

```json
{
  "SuppFormMode": 1,
  "ItemUid": "item_1",
  "RequiredPlugin": "plugin_code",
  "SQlCondition": "SELECT 1 WHERE EXISTS (SELECT * FROM table WHERE condition)"
}
```

- **SuppFormMode** (required): An integer representing the form modes. Must be between -1 and 15.
- **ItemUid**: Specifies the item UID for which the menu is available when clicked.
- **SQlCondition**: A SQL query that, when returns an integer 1, makes the menu available. Parameters in the query must match those defined in the `Parameters` array.
- **RequiredPlugin**: The Plugin Code that is required to be enabled for this menu to be available.

### UIParameter

Defines parameters used by the UI element. A `UIParameter` JSON object might look like:

```json
{
  "Name": "param1",
  "Type": "string",
  "Row": false,
  "TableName": "Orders",
  "ColumnName": "OrderID",
  "SQLTransformQuery": "SELECT Column FROM Table WHERE Condition = @param2"
}
```

- **Name** (required): The name of the parameter.
- **Type** (required): The type of the parameter ( `string`,`int`,`float`,`date`) are available.
- **Row**: A boolean indicating if the parameter's value comes from the current row (`true`) or the first row (`false`) (header halues).
- **TableName**: Required unless `SQLTransformQuery` is provided. Specifies the table from which the parameter's value is retrieved if Row is false and a Matrix or grid Name when Row is true.
- **ColumnName**: Required unless `SQLTransformQuery` is provided. Specifies the column from which the parameter's value is retrieved if Row is false and  matruix ColumnUid or DataTableColume name in grid  when row is tue .
- **SQLTransformQuery**: An optional SQL query that overrides `TableName` and `ColumnName`. It must use parameters listed in the `Parameters` array.

### Validation Logic

- All required fields must be provided.
- `FormType`, `MenuCaption`, and `MenuUid` are required based on the `Type` of the UI element.
- `SuppFormMode` in `UICondition` must be between -1 and 15. its a mask of form modes.
  - -1: Always visible
  - 1: OK
  - 2: Add
  - 4: Find
  - 8: View
  
- `SQLTransformQuery` in both `UIParameter` and `UICondition` must only use parameters that are defined in the `Parameters` array of the `UiDescription`.
- For `ContextMenu` type, both `MenuCaption` and `FormType` are required, and a `Condition` must be provided.

Ensure that the JSON configurations prepared for these entities adhere to the above specifications and validation rules to avoid errors and ensure the correct functioning of the UI elements.



## Examples
* context menu on sales order form that for selected row checks if Item Code start with a and opens a google search for item code 
```json

      {
        "Type": "ContextMenu",
        "Title": "StartWithATest",
        "FormType": "139",
        "MenuUid": "SOMatrixRow",
        "InternalUrl": false,
        "MenuCaption": "StartWithATest",
        "Url": "https://www.google.com/search?q=@ItemCode",
        "Condition": {
          "SuppFormMode": -1,
          "SQlCondition": "select  case when Left(@ItemCode,1) ='A' then 1 else 0 end  FROM DUMMY",
          "ItemUid": "38"
        },
        "Parameters": [
          {
            "Name": "ItemCode",
            "Type": "string",
            "TableName": "38",
            "ColumnName": "1",
            "Row": true
          }
        ]
      }
```
* context menu on Item Master data that checks if ItemCode start with A  
```json
{
        "Type": "ContextMenu",
        "Title": "StartWithATest",
        "FormType": "150",
        "MenuUid": "AEPlug12s3",
        "InternalUrl": false,
        "MenuCaption": "StartWithATest",
        "Url": "https://www.onet.pl/",
        "Condition": {
          "SuppFormMode": -1,
          "SQlCondition": "select  case when Left(@ItemCode,1) ='A' then 1 else 0 end  FROM DUMMY"
        },
      "Parameters": [
        {
          "Name": "ItemCode",
          "Type": "string",
          "TableName": "OITM",
          "ColumnName": "ItemCode"
        }
      ]
      }

```