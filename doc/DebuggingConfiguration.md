### Computec Start

### AppEngine 

### Plugin dev.config.json
This file descripe the plugin development configuration and its dependent plugins 
* SourceDirectory relative path from the dev.config.json file or absolute to the source directory of a plugin 
* Output dll relative path from SourceDirectory to the output dll
* Dependencies= list of delpendent plugins ( ling to dev.config.json) relative from SourceDirectory
* MapWwwAppFolder FOR ae plugins if www sources are loaded from resurces or from the sources 
* 
BL Plugin example
```json
{
  "SourceDirectory": "",
  "OutputDll": "bin\\Debug\\net8.0\\CT.VehOne.BL.dll",
  "Dependencies": [],
  "MapWwwAppFolder": false
}

```
SAP UI Plugin Example
```json
{
  "SourceDirectory": "",
  "OutputDll": "bin\\Debug\\net8.0\\CT.VehOne.dll",
  "MapWwwAppFolder":true,
  "Dependencies":["..\\CT.VehOne.BL\\dev.config.json"]
}
```
AE Plugin Example 
```json
{
  "SourceDirectory": "",
  "OutputDll": "bin\\Debug\\net8.0\\CT.VehOne.dll",
  "Dependencies": [
    "..\\CT.VehOne.BL\\dev.config.json"
  ],
  "MapWwwAppFolder": true
}
 
```
