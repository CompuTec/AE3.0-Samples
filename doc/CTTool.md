
# Installing the CompuTec Tool

To install the CompuTec tool, run the following command:

```batch
dotnet tool install CompuTec.Tool  
```

## Generate Models

To generate models, use:

```batch
dotnet ct gen
```

If the required options are missing, you will see an error similar to this:

```
ERROR(S):
  Required option 's, SourceDll' is missing.
  Required option 'd, Destination' is missing.
  Required option 'n, PluginNamespace' is missing.
```

Below are the command options for generating models:

- **-b, --Debugger**: Attach debugger.
- **-l, --Log**: Log to information.
- **-s, --SourceDll** (Required): Source Business Logic DLL.
- **-d, --Destination** (Required): Plugin Destination Source Directory (e.g., "C:\git\ConsolePlugin\Sources").
- **-m, --ModelDestination**: Destination Source Model Directory.
- **-i, --SerializationDestination**: Destination Source Serializers Directory.
- **-n, --PluginNamespace** (Required): Plugin Namespace.
- **-e, --ModelNamespace**: Model Namespace.
- **-r, --SerializerNamespace**: Serializer Namespace.
- **-p, --PluralEndpointNames**: (Default: false) Plural endpoints names.
- **-a, --All**: (Default: false) Generate all.
- **-g, --generators**: Custom Generators (e.g., `s`-serializer, `o`-ODataController, `m`-models, `c`-REST controllers).
- **-w**: Serialize models for generation analysis.

## Generate Plugin

To create a plugin package for manual installation, use:

```batch
dotnet ct pack
```

### Example Error Message

If required values are missing, you may see an error like this:

```
ERROR(S):
  A required value not bound to option name is missing.
  Directory (pos. 0)    Required. Directory to pack or unpack.
```

The command options are:

- **-s**: Save in same directory.
- **-d**: Attach debugger.
- **-v**: Validate DLL.
- **-r**: Version to set.
- **-x**: Dependencies separated by `;` that must have the same minimal version (e.g., `"ConsoleApp.DI;ConsoleApp.Core"`).
- **-o**: Only replace manifest file.

## Translating SAP Forms

To generate translations for SAP forms:

```batch
dotnet ct GenerateTranslations
```

If required options are missing, the error may look like this:

```
ERROR(S):
  Required option 's' is missing.
  Required option 'r' is missing.
  Required option 't' is missing.
```

Options for generating translations:

- **-s** (Required): Sources Directory.
- **-r** (Required): Root Directory.
- **-t** (Required): Form Translation File.

## Creating Plugin Package for Manual Installation

Use:

```batch
dotnet ct pack
```

If required values are missing:

```
ERROR(S):
  A required value not bound to option name is missing.
  Directory (pos. 0)    Required. Directory to pack or unpack.
```

## Creating NuGet Package for Plugin Repository

To push a NuGet package to the plugin repository, use:

```batch
dotnet ct push
```

If required values are missing, you might see:

```
ERROR(S):
  A required value not bound to option name is missing.
```

Options:

- **-r**: Version to set.
- **-k**: API Key.
- **-s**: Source.
- **-x**: Dependencies with minimal version.

## Setting Up the .csproj File to Manage Required Tasks

### For Business Logic Plugin

1. Generate models, serializers, and other components.
2. Create a plugin zip file and NuGet package on release.

```xml
<Target Name="GenerateSerializers" AfterTargets="AfterBuild" Condition="'$(TargetFramework)' == 'net8.0'">
    <Message Importance="High" Text="Generate Models Serializers etc $(ProjectDir) "/>
    <Exec WorkingDirectory="$(TargetDir)" Command="del *.zip /F"/>
    <Exec WorkingDirectory="$(TargetDir)" Command="del *.nupkg /F"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet tool restore"/>
    <Exec Command="dotnet ct gen --Destination $(OneUpFolder)\CT.VehOne\ -l -g scmo -s $(TargetPath) -n CT.VehOne" />
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct push $(TargetDir) -r $(Version) -z " Condition="'$(Configuration)'!='Debug'"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct pack $(TargetDir) -r $(Version)"/>
</Target>
```

### For SAP UI Plugin

1. Generate translated forms on release.

```xml
<Target Name="CTpack" AfterTargets="AfterBuild">
    <Exec WorkingDirectory="$(TargetDir)" Command="del *.zip /F"/>
    <Exec WorkingDirectory="$(TargetDir)" Command="del *.nupkg /F"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet tool restore"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct push  $(TargetDir) -x CT.VehOne.BL -r $(Version) -z" Condition="'$(Configuration)'!='Debug'"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct pack $(TargetDir) -x CT.VehOne.BL -r $(Version)"/>
</Target>
```

### For AppEngine Plugin

1. Generate plugin zip file and NuGet package on release.

```xml
<Target Name="CTpack" AfterTargets="AfterBuild">
    <Message Text="AfterBuild"/>
    <Exec WorkingDirectory="$(TargetDir)" Command="del *.zip /F"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet tool restore"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct push  $(TargetDir) -r $(Version)  -x CT.VehOne.BL -s $(ComputecPluginSource) -z" Condition="'$(Configuration)'!='Debug'"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct pack $(TargetDir) -r $(Version) -x CT.VehOne.BL"/>
</Target>
```
