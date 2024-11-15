
# Dev.config Guide

The `CompuTec.Core` framework utilizes a configuration file (`Dev.config`) loaded at startup. This file enables features like debugging plugins, enabling telemetry, configuring logging, and customizing runtime behavior.

---

## Logging Configuration

You can configure logging settings using the following structure:

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

---

## Development Configuration

The `DevelopmentConfiguration` section is crucial for debugging plugins. When enabled, it loads all plugins listed in the `Plugins` array. Each path must point to a valid `dev.config.json` file that describes the plugin’s location and dependencies.

### Example

```json
"DevelopmentConfiguration": {
    "Enabled": true,
    "Plugins": [
        "C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\dev.config.json"
    ]
}
```

---

## Debugging SAP Business One with CompuTec Start

### Requirements

- Ensure the company is activated in AppEngine.
- Install the `CompuTec.Start` add-on. By default, it is located at:
  `C:\Program Files\sap\SAP Business One\AddOns\CT\CompuTec.Start\X64Client\`

### Steps

1. Prepare a `dev.config.json` file for `CompuTec.Start`. This file can include logging, telemetry, properties, and force development mode configurations to load your plugin.

2. Define a launch profile with the `CT_DEBUG` environment variable pointing to the `dev.config.json` file.

#### Example: `launchSettings.json`

```json
{
  "profiles": {
    "LaunchSApUiPlugin": {
        "commandName": "Executable",
        "executablePath": "C:\Program Files\sap\SAP Business One\AddOns\CT\CompuTec.Start\X64Client\CompuTec.Start.exe",
        "workingDirectory": "C:\Program Files\sap\SAP Business One\AddOns\CT\CompuTec.Start\X64Client\",
        "environmentVariables": {
            "CT_DEBUG": "C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\Properties\Debug.config.json"
        }
    }
  }
}
```

#### Example: `Debug.config.json`

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
        "C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\dev.config.json"
    ]
  }
}
```

---

## Debugging AppEngine

### Steps to Prepare AppEngine for Debugging

1. Stop the AppEngine service.
2. Navigate to the AppEngine directory:
   `C:\Program Files\CompuTec AppEngine`
3. Edit the `DevApp.template` file:
    - Specify the database connection string.
    - Define a unique Dev Instance GUID.
    - Assign a name to this instance.
    - Optionally adjust logging settings.
    - Save the file as `DevApp.config.json`.

#### Example: `DevApp.config.json`

```json
{
  "ConnectionStrings": {
    "DefaultDbProvider": "Hana",
    "DefaultConnection": "Database=DEV;server=hanadev:30013;uid=SYSTEM;password=*8*;current Schema=CTAPPDATA"
  },
  "DevelopmentConfiguration": {
    "Enabled": true,
    "Plugins": []
  },
  "AppEngineInstance": "e5aaf3d2-626c-4eea-a206-48979edee0e7",
  "DefaultAppEngineInstanceName": "Development Server"
}
```

4. Run the `CompuTec.AppEngine.Host.DevApp.core.exe` file as Administrator.
5. Activate the required companies via `https://localhost:54001`.
6. Stop the application if you wish to run it from a development environment.

---

## Debugging Web Plugins with AppEngine

Prepare a `launchSettings.json` file for debugging web plugins.

### Example: `launchSettings.json`

```json
{
  "profiles": {
    "Default": {
      "commandName": "Executable",
      "executablePath": "C:\Program Files\CompuTec AppEngine\CompuTec.AppEngine.Host.DevApp.core.exe",
      "workingDirectory": "C:\Program Files\CompuTec AppEngine",
      "environmentVariables": {
        "CT_DEBUG": "yourProjectPath\Properties\DevAeConfig.config.json"
      }
    }
  }
}
```

### Example: `DevAeConfig.config.json`

```json
{
  "Logging": {
    "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
    },
    "Console": {
        "IncludeScopes": true
    },
    "Debug": {
        "IncludeScopes": true
    }
  },
  "DevelopmentConfiguration": {
    "Enabled": true,
    "Plugins": [
        "C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne\dev.config.json"
    ]
  }
}
```

This guide provides a comprehensive reference for configuring and debugging SAP Business One plugins using `CompuTec.Core`.
