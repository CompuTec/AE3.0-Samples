# Basics and business Logic 
Example application can be found in [github]()
## Plugin Types 
| Plugin Type Description | `ApplicationTypes` Enum Value | Acceptable String Representations |Core References|PluginInfo base class|
|-------------------------|-------------------------------|-----------------|-|-----------------|
| Business Logic          | BusinessLogic                 | "BusinessLogic", "DiAPI", "Di"    |CompuTec.Core2|PluginInfo|
| SAP User Interface      | SapUserInterface              | "SapUserInterface", "SapUI", "UI" |CompuTec.Core2.UI|SapUiPluginInfo|
| App Engine Plugin       | AppEnginePlugin               | "AePlugin", "plugin", "AEComponent", "AppEnginePlugin" | CompuTec.Core2.AE |AEPlugin|
| Standalone              | Standalone                    | "Standalone", "exe", "app"        |
| Other                   | Other                         | "Other"                           |


- [About Framework ](aboutframework.md)
- [Before you Start](beforeYouStart.md)
- [Dependency Injection](dependencyInjection.md)
- [Manifest](manifest.md)
- [Datbase Structure setup](DatabaseStructureSetup.md)
- [User Define Objects](UdoBeans.md)
- [Message Translations](baseTranslations.md)
# Sap User Interface 
- [Connection Properties](connectionProperties.md)
- [User interface UI Model](BusinessLogicUiModel.md)
- [Events](UiEvents.md)
- [Forms](UiForms.md)
- [Form transations](fromTranslations.md)
- [ActiveX setup](AciveXInsallationGuide.md)
# AppEngine Plugin
- [Set up web Model generator](generator.md)
- [Extending the Generated Controlers](extendAEControlers.md)
- [Creating Own Controlers](extendAEControlers.md)
- [Jobs](AeJobs.md)
- [Hosting web content in sap UI](AEContentInSAPUIs.md)
- [Simple Views](simpleView.md)
- [SAP UI 5 in AppEngine](AEUI.md)

# Dev tool
[dotnet ct](CTTool.md)