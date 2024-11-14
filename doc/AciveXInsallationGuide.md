
# ActiveX Support in Computec.AppEngine Ecosystem

> **Note:** This feature works only with the SAP User Interface plugin type.

The ActiveX mechanism checks if the component is installed. If not, it attempts to install it. In cases where the SAP Business One application lacks sufficient privileges, a message will appear on the screen, and a batch command will be copied to the clipboard.

The user will then need to:
1. Open Command Prompt as an administrator.
2. Paste the command and press **Enter**.

## How to Add ActiveX Support

1. In the `PluginInfo`, add the following line:

    ```csharp
    ActiveXComponents = new ActiveXInstallDefinition[]
    {
        new ("7B3C32C6-2856-1AB9-A132-CA5FBCC90115",
            "ProcessForce.Gantt\CompuTec.ProcessForce.Gantt.dll")
    };
    ```

    where:
    - **GUID** is the CLSID of the ActiveX component.
    - **Path** is a relative path to the plugin manifest.

### Update Mechanism

The system compares the version of the installed library with the version from the specified path to detect if an update of the ActiveX component is required.
