# 💀 SkeletonKit Localization Mongo

## Description
Localization support for MongoDB.

## Usage
1. Replace a `string` property in your entity class with `LocalizedString`:
```  
public sealed class Countries
{
	public string Id { get; set; }
	public string Code { get; set; }
	public LocalizedString Name { get; set; }
}
```

This represents a MongoDB document with the following structure:
```
{
  "_id": "1",
  "Name": {
    "en": "Afghanistan",
    "en-UK": "...",
    "ar": "...",
    "fr" : "...",
    "jp": "...",
  },
  "Code": "AF"
}
```

2. Register the new field using `MapLocalizedStringField` extension function: 
```
if (!BsonClassMap.IsClassMapRegistered(typeof(Countries)))
{
    BsonClassMap.RegisterClassMap<Countries>(cm =>
    {
        cm.AutoMap();
        cm.MapIdField(x => x.Id);
        cm.MapLocalizedStringField(x => x.Name);
    });
}
```
