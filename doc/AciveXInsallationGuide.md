# ActiveX support in Computec.AppEngine Ecosystem
## note: it works only with the sap User interface plugin type 
The mechanism will check if the active X is isntalled and if not will try to isntall. In case when the sapb1 application will not have sufficent priviliges the message will be displayed on the screen and the batch command will be added to the clipboard.
The user will have to :
1) open cmd as administrator 
2) paste the command and pres [Enter]

## how to add ActiveX support 
1) in the PluginInfo please add the following Line 
```cs
  ActiveXComponents = new ActiveXInstallDefinition[]
        {
            new ("7B3C32C6-2856-1AB9-A132-CA5FBCC90115",
                "ProcessForce.Gantt\\CompuTec.ProcessForce.Gantt.dll")
        };
```
 where: 
* GUID is the CLID of the AactiveX 
* path is a relative path to the plugin manifest.
### Update :
The system will compare the version of the installed library with the one from the path to detect if an update of the ActiveX is required.