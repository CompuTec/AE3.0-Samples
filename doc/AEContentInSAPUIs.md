
# JSON Configurations for SAP Business One Application Menu and Context Menu

This document provides a structure for configuring menu and context menu entries in SAP Business One. These entries can open web-based pages and applications, allowing integration with external resources. Through these configurations, you can add a main menu or context menu to existing forms, extract data from those forms, validate the data, generate URLs, and open web content within SAP Business One.

## General Structure

Each UI entity (`UiDescription`, `UICondition`, `UIParameter`) is represented as a JSON object. The fields within each object define properties of that entity, and the values must follow the specified rules.

### UiDescription

Defines a UI element like a form, context menu, or analysis item. Here’s an example structure:

```json
{
  "Type": "Form",
  "FormType": "frm_sales_order",
  "MenuUid": "unique_sales_order",
  "Url": "https://example.com",
  "MenuCaption": "Sales Order",
  "MenuCaptionTranslationId": "Menu_Translation_Id",
  "TitleTranslationId": "Title_Translation_Id",
  "Title": "Sales Order",
  "Condition": { ... },
  "Parameters": [ ... ]
}
```

- **Type** (required): Specifies `"Form"`, `"ContextMenu"`, or `"Analytics"` to define the UI element type.
- **FormType**: Unique identifier for forms; required for `Type` set to `"ContextMenu"`.
- **MenuUid** (required): Unique identifier for the UI element.
- **FatherMenuUid**: Specifies the parent element; required if `Type` is `"Form"`.
- **Url** (required): URL associated with the UI element.
- **InternalUrl**: Internal URL for a Plugin hosted in AppEngine.
- **MenuCaption**: Display text in the menu; required for `"ContextMenu"` and `"Form"`.
- **Title**: UI element title; required for all types.
- **Condition**: Specifies visibility conditions for `"ContextMenu"`.
- **Parameters**: Array of `UIParameter` objects defining parameters.
- **MenuCaptionTranslationId**: Translation ID for `MenuCaption`.
- **TitleTranslationId**: Translation ID for `Title`.

### UICondition

Specifies conditions for UI element visibility. Example structure:

```json
{
  "SuppFormMode": 1,
  "ItemUid": "item_1",
  "RequiredPlugin": "plugin_code",
  "SQLCondition": "SELECT 1 WHERE EXISTS (SELECT * FROM table WHERE condition)"
}
```

- **SuppFormMode** (required): Integer for form modes, from -1 to 15.
- **ItemUid**: Specifies the item UID for menu availability.
- **SQLCondition**: SQL query that returns `1` if the menu should be available; parameters must match those defined in `Parameters`.
- **RequiredPlugin**: Plugin Code required for the menu to be available.

### UIParameter

Defines parameters for the UI element. Example structure:

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

- **Name** (required): Parameter name.
- **Type** (required): Parameter type (`string`, `int`, `float`, `date`).
- **Row**: Boolean indicating if the value comes from the current row (`true`) or first row (`false`).
- **TableName**: Required unless `SQLTransformQuery` is provided; specifies table source for parameter value.
- **ColumnName**: Required unless `SQLTransformQuery` is provided; specifies column source for parameter value.
- **SQLTransformQuery**: Optional SQL query to override `TableName` and `ColumnName`.

### Validation Logic

- **Required Fields**: All required fields must be provided.
- **Type-Specific Fields**: Fields such as `FormType`, `MenuCaption`, and `MenuUid` are required depending on the UI element type.
- **SuppFormMode**: Value must be from -1 to 15:
  - -1: Always visible
  - 1: OK
  - 2: Add
  - 4: Find
  - 8: View

- **SQLTransformQuery**: Should use only parameters defined in the `Parameters` array of the `UiDescription`.
- **ContextMenu Requirements**: `MenuCaption`, `FormType`, and `Condition` must be provided.

Ensure JSON configurations meet these specifications and validation rules for error-free, functional UI elements.

### Examples

#### Example 1: Context Menu on Sales Order Form

This context menu is displayed for a selected row if the `ItemCode` starts with "A" and opens a Google search for the item code.

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
    "SQLCondition": "SELECT CASE WHEN LEFT(@ItemCode, 1) = 'A' THEN 1 ELSE 0 END FROM DUMMY",
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

#### Example 2: Context Menu on Item Master Data

This context menu is displayed on the Item Master Data form if `ItemCode` starts with "A".

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
    "SQLCondition": "SELECT CASE WHEN LEFT(@ItemCode, 1) = 'A' THEN 1 ELSE 0 END FROM DUMMY"
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

This document ensures that each JSON configuration complies with SAP Business One requirements for seamless integration with external applications.
