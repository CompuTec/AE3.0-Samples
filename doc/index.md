
# Basics and Business Logic

Example applications can be found in [GitHub]()

## Plugin Types

| Plugin Type Description | `ApplicationTypes` Enum Value | Acceptable String Representations | Core References       | PluginInfo Base Class |
|-------------------------|-------------------------------|-----------------------------------|-----------------------|------------------------|
| Business Logic          | BusinessLogic                 | "BusinessLogic", "DiAPI", "Di"    | CompuTec.Core2        | PluginInfo             |
| SAP User Interface      | SapUserInterface              | "SapUserInterface", "SapUI", "UI" | CompuTec.Core2.UI     | SapUiPluginInfo        |
| App Engine Plugin       | AppEnginePlugin               | "AePlugin", "plugin", "AEComponent", "AppEnginePlugin" | CompuTec.Core2.AE | AEPlugin               |
| Standalone              | Standalone                    | "Standalone", "exe", "app"        |                       |                        |
| Other                   | Other                         | "Other"                           |                       |                        |

## Framework Documentation

- [About Framework](aboutframework.md)
- [Before you Start](beforeYouStart.md)
- [Dependency Injection](dependencyInjection.md)
- [Manifest](manifest.md)
- [Database Structure Setup](DatabaseStructureSetup.md)
- [User-Defined Objects](UdoBeans.md)
- [Message Translations](baseTranslations.md)
- [Configurations](Configurations.md)

## SAP User Interface

- [Connection Properties](ConnectionProperties.md)
- [User Interface UI Model](BusinessLogicUiModel.md)
- [Events](UiEvents.md)
- [Forms](UiForms.md)
- [Form Translations](FromTranslations.md)
- [ActiveX Setup](AciveXInsallationGuide.md)

## AppEngine Plugin

- [Set Up Web Model Generator](generator.md)
- [Extending the generated controllers](ExtendUdoControllers.md)
- [Creating controllers](AEControlers.md)
- [Jobs](AeJobs.md)
- [Hosting Web Content in SAP UI](AEContentInSAPUIs.md)
- [Simple Views](simpleView.md)
- [SAP UI 5 in AppEngine](AEUI.md)

## Dev Tools

- [dotnet ct](CTTool.md)
- [Debugging Your Code](DebuggingConfiguration.md)
