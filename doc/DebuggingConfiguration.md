## Dev.config
CompuTec.Core have uses a configuration file that is loaded on the startup that can change some befaviours and allow the user to debug a plugin, enable telemetry , etc

### Logging
you can configura your logging 
```json
 "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    },
    "Console": {
      "IncludeScopes": true
    },
    "NLog": {
      "IncludeScopes": true,
      "RemoveLoggerFactoryFilter": false
    }
 }
```
### DevelopmentConfiguration 
this part is crutial for the debugging plugins system after if DevelopmentConfiguration is ebabled will load all the plugins that are listed in the `Plugin` array the path must lead to the [dev.config.json](dev.config.json.md) file witch descripte the plugin location and depenedencies.
 ```json
 "DevelopmentConfiguration": {
    "Enabled": true,
    "Plugins": [
      "C:\\git\\core\\SampleApp\\CT.VehOne\\Source\\CT.VehOne.UI\\dev.config.json"
    ]
  }
```
### Debus SAP UI with Computec Start
To debug a SAP Business One User interface Plugin you must first have Company Activated in AppEngine. After activation CompuTec.Start addon will be availble to run and will be installed `c:\Program Files\sap\SAP Business One\AddOns\CT\CompuTec.Start\X64Client\`

you need to prepare a dev.config.file that will be readed by the CompuTec.Start application. in this file we can define logging , letemetry , Properties and Force dev mode that will load your plugin 

you need to prepare a lauch profile that will set CT_DEBUG envirement variable that willhave a path to youd Debug.config.json file 
launchSettings.json
```json
{
  "profiles": {
     "LaunchSApUiPlugin":{
      "commandName": "Executable",
      "executablePath": "C:\\Program Files\\sap\\SAP Business One\\AddOns\\CT\\CompuTec.Start\\X64Client\\CompuTec.Start.exe",
      "workingDirectory": "C:\\Program Files\\sap\\SAP Business One\\AddOns\\CT\\CompuTec.Start\\X64Client\\",
      "environmentVariables": {
        "CT_DEBUG": "C:\\git\\core\\SampleApp\\CT.VehOne\\Source\\CT.VehOne.UI\\Properties\\Debug.config.json"
      }
}
  }
  }
```
example Debug.config.json file
```json
{
  "ConnectionProperties": {
    "FORMCachingDisabled": true,
    "FofseDbStructureCheckOnConnectionInitialization": true,
    "PluginDirectory": ""
  },
  "DevelopmentConfiguration": {
    "Enabled": true,
    "AeWwwPath": "",
    "Plugins": [
      "C:\\git\\core\\SampleApp\\CT.VehOne\\Source\\CT.VehOne.UI\\dev.config.json"
    ]
  }

}
```

### Debug Web Plugins with AppEngine 

