#Message Translations:
It applys for all the plugin types 
you can define translation.xml file that will hold all the message translations that later can be used in the all plugins 

## Translation file stricture
```xml
  <Message id="CT_PF_CannotUpdateBOM" param="2">
    <translation lang="Default">Cannot update Bill of Material. Code: {0}. Error: {1}.</translation>
    <translation lang="ln_English">Cannot update Bill of Material. Code: {0}. Error: {1}.</translation>
    <translation lang="ln_Polish">Nie można zaktualizować Receptury. Kod: {0}. Błąd: {1}.</translation>
  </Message>
```

**id** please Generate id that will be unique based on the [standard](UIDSTablesFIeldRules.md)
**param** number of parrameters in the translation
**lang** lanuage :
- "Default"
- ln_English"
- ln_Polish"
- ln_Hebrew"
- ln_German"
- ln_Danish"
- ln_Norwegian"
- ln_Italian"
- ln_Hungarian"
- ln_Chinese"
- ln_Dutch"
- ln_Finnish"
- ln_Greek"
- ln_Portuguese"
- ln_Swedish"
- ln_French"
- ln_Spanish"
- ln_Russian"
- ln_Spanish_La"
- ln_Czech_Cz"
- ln_Slovak_Sk"
- ln_Korean_Kr"
- ln_Portuguese_Br"
- ln_Japanese_Jp"
- ln_Turkish_Tr"
- ln_TrdtnlChinese_Hk"
- ln_English_Gb"
- ln_Serbian"
- ln_Spanish_Ar"
- ln_Spanish_Pa"
- ln_Ukrainian"
## Manifest declaration
when you want to define a message translation file in the manifest please add this section to manifest.json
and the messageTranslations file mark as ebleded resource
```json
 { "TranslationDescription":
  {
    "XmlTranslationFile":"CT.VehOne.BL.Translations.MessageTranslations.xml"
  }
 }
```
**XmlTranslationFile** is the path to the embleded resource
## plugin Declaration
You can also define the message translations in the plugin Info class 
```csharp
public MyPluginInfo()
{
    TranslationDescription="www.mytranslation.xml"
    TranslationDescription.TranslationInvoker = getTranslationsFromWWW;
}
```

## Using Translations in code
```csharp

internal class MyService
{

  	private readonly  Application _application;
  private readonly ITrasnlationService _translationSercie;
  public MyService (ITrasnlationService translationSercie,AppHolder appHolder)
  {
      _translationSercie=translationSercie;
      _applcation= appHolder.App ;
  }
  public void MyMeod()
  {
      _applcation.MessageBox(_translationSercie.GetTranslatedMessage("CT_XX_MyTrasnId");
  }
}
```
you can also get the service from **ICoreConnection**
```csharp
public void MyMeod()
  {
      var trService= _coreConnection.GetSerivce<ITrasnlationService>();
      _applcation.MessageBox(trService.GetTranslatedMessage("CT_XX_MyTrasnId");
  }
```