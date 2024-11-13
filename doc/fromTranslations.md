
# Translating Form

Form translations are usually stored in `FormTranslations.xml`. Using the [ct tool](ct.tool.md), you can:

- Create or update translation files based on the form XML definitions.
- Generate translated forms, which is typically part of the build process.

To translate the forms, please fill in the `FormTranslations.xml` with appropriate values.

## Generating the FormTranslations.xml File

To generate the `FormTranslations.xml` file, use the following command:

```batch
dotnet ct GenerateTranslations -s C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\UI\Forms -r C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\ -t C:\git\core\SampleApp\CT.VehOne\Source\CT.VehOne.UI\UI\Translations\FormTranslations.xml
```

- **-s : Source** - The folder that contains the form definitions.
- **-r : Project Root Directory** - The project’s root directory.
- **-t : Output Translations** - The output directory for translations.

## Generating the Translated Forms

It is recommended to include this step in the SAP UI project to automatically trigger form generation.

```xml
<Target Name="Translate" AfterTargets="AfterBuild">
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet tool restore"/>
    <Exec WorkingDirectory="$(ProjectDir)" Command="dotnet ct translate -s $(ProjectDir) -t UI\Translations\FormTranslations.xml" />
</Target>
```

## Tips

When working with forms, add `FORMCachingDisabled` in the [connectionProperties](connectionProperties.md) configuration. This ensures that files in `computec.start` are not cached, allowing the default form from the output directory to load in the UI every time.
