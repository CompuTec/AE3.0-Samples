
# Message Translations

Message translations apply to all plugin types. You can define a `translations.xml` file that holds all message translations, allowing consistent multilingual support across plugins.

## Translation File Structure

```xml
<Message id="CT_PF_CannotUpdateBOM" param="2">
    <translation lang="Default">Cannot update Bill of Material. Code: {0}. Error: {1}.</translation>
    <translation lang="ln_English">Cannot update Bill of Material. Code: {0}. Error: {1}.</translation>
    <translation lang="ln_Polish">Nie można zaktualizować Receptury. Kod: {0}. Błąd: {1}.</translation>
</Message>
```

- **id**: Unique identifier for the message, generated according to the [standard](UIDSTablesFIeldRules.md).
- **param**: Number of parameters in the translation.
- **lang**: Language code, with options including:
  - `Default`
  - `ln_English`
  - `ln_Polish`
  - `ln_Hebrew`
  - `ln_German`
  - `ln_Danish`
  - `ln_Norwegian`
  - `ln_Italian`
  - `ln_Hungarian`
  - `ln_Chinese`
  - `ln_Dutch`
  - `ln_Finnish`
  - `ln_Greek`
  - `ln_Portuguese`
  - `ln_Swedish`
  - `ln_French`
  - `ln_Spanish`
  - `ln_Russian`
  - `ln_Spanish_La`
  - `ln_Czech_Cz`
  - `ln_Slovak_Sk`
  - `ln_Korean_Kr`
  - `ln_Portuguese_Br`
  - `ln_Japanese_Jp`
  - `ln_Turkish_Tr`
  - `ln_TrdtnlChinese_Hk`
  - `ln_English_Gb`
  - `ln_Serbian`
  - `ln_Spanish_Ar`
  - `ln_Spanish_Pa`
  - `ln_Ukrainian`

## Manifest Declaration

To define a message translation file in the manifest, add the following section to `manifest.json`, marking the `messageTranslations` file as an embedded resource:

```json
{
  "TranslationDescription": {
    "XmlTranslationFile": "CT.VehOne.BL.Translations.MessageTranslations.xml"
  }
}
```

- **XmlTranslationFile**: Path to the embedded resource.

## Plugin Declaration

You can also define message translations in the plugin `Info` class:

```csharp
public MyPluginInfo()
{
    TranslationDescription = "www.mytranslation.xml";
    TranslationDescription.TranslationInvoker = getTranslationsFromWWW;
}
```

## Using Translations in Code

To use translations in your code:

```csharp
internal class MyService
{
    private readonly Application _application;
    private readonly ITranslationService _translationService;

    public MyService(ITranslationService translationService, AppHolder appHolder)
    {
        _translationService = translationService;
        _application = appHolder.App;
    }

    public void MyMethod()
    {
        _application.MessageBox(_translationService.GetTranslatedMessage("CT_XX_MyTransId"));
    }
}
```

You can also retrieve the service from **ICoreConnection**:

```csharp
public void MyMethod()
{
    var trService = _coreConnection.GetService<ITranslationService>();
    _application.MessageBox(trService.GetTranslatedMessage("CT_XX_MyTransId"));
}
```
