
# PluginDescription JSON Configuration

This guide provides instructions for configuring and validating a `PluginDescription` entity through a JSON manifest. It enables users to specify plugin characteristics, dependencies, and validation rules for integrating plugins into an application.

The file must be called **manifest.json**

## PluginDescription JSON Structure

The JSON manifest for a plugin provides detailed information about the plugin, its dependencies, and how it integrates with the application ecosystem. Here’s an example structure with field descriptions:

```json
{
  "PluginCode": "Core.Example01.AE",
  "PluginName": "Example Plugin",
  "Icon": "sap-icon://activate",
  "Assembly": "ConsoleApp.AE.dll",
  "Company": "Your Company Name",
  "PluginVersion": "1.0.5",
  "Remarks": "Example Plugin Remarks that will be displayed in the plugin list",
  "ReadmeFile": "README.md",
  "ReleaseNotes": "RELEASE_NOTES.md",
  "DocumentationLink": "http://example.com/plugin-docs",
  "PluginType": "AppEnginePlugin",
  "MinimumCoreLevel": "1.0.0",
  "Dependencies": [
    {
      "PluginCode": "Core.Example01.API",
      "minVersion": "1.0"
    }
  ]
}
```

## Field Descriptions and Validation Rules

- **PluginCode** (required): The unique code for the plugin. Must be a non-empty string.
- **PluginName** (required): The name of the plugin. Must be a non-empty string.
- **Icon** (optional): Specifies the icon for the plugin, often as a URI or a reference to an icon library.
- **Assembly** (required): The filename of the assembly containing the plugin. Must be a non-empty string.
- **Company** (optional): The name of the company or individual who owns the plugin.
- **PluginVersion** (required): Version of the plugin, following semantic versioning (must be in a valid `CTVersion` format).
- **Remarks** (optional): Additional information about the plugin.
- **ReadmeFile** (optional): Path to the README file for the plugin.
- **ReleaseNotes** (optional): Path to the release notes file for the plugin.
- **DocumentationLink** (optional): A URL pointing to the plugin’s documentation.
- **PluginType** (required): Type of plugin, which must be one of the following: `Standalone`, `BusinessLogic`, `AppEnginePlugin`, `SapUserInterface`, `Other`.
- **MinimumCoreLevel** (required): Minimum version of the core application required for the plugin (must follow `CTVersion` format).
- **Dependencies** (optional): Array of objects specifying the plugin dependencies, where each object includes:
    - **PluginCode**: The dependent plugin's code.
    - **minVersion**: Minimum required version of the dependent plugin.

## Plugin Type

The `PluginType` field categorizes the plugin, determining how it’s deployed, integrated, and used within the application ecosystem.

### Conversion Logic

Conversion between string representations and `ApplicationTypes` enum values ensures compatibility across different environments. Below is a table with the supported plugin types, descriptions, and acceptable string representations:

| Plugin Type Description | `ApplicationTypes` Enum Value | Acceptable String Representations |
|-------------------------|-------------------------------|-----------------------------------|
| Business Logic          | BusinessLogic                 | "BusinessLogic", "DiAPI", "Di"    |
| SAP User Interface      | SapUserInterface              | "SapUserInterface", "SapUI", "UI" |
| Standalone              | Standalone                    | "Standalone", "exe", "app"        |
| App Engine Plugin       | AppEnginePlugin               | "AePlugin", "plugin", "AEComponent", "AppEnginePlugin" |
| Other                   | Other                         | "Other"                           |

## Validation Logic

- **PluginCode** and **Assembly** fields must not be empty. `PluginCode` serves as a unique identifier, while `Assembly` specifies the main assembly.
- **PluginVersion** and **MinimumCoreLevel** must be in a valid version format to ensure correct handling of version dependencies. Follows [Semantic Versioning](https://semver.org/).
- **PluginType** must match one of the predefined types for correct integration into the application ecosystem.
- **Dependencies** (if specified): Each dependency must include both `PluginCode` and `minVersion` to verify compatibility with necessary components.

> Adding a detailed section on Plugin Types and their conversion helps clarify plugin categorization and transformation within the system, especially for serialization/deserialization and NuGet package management.
