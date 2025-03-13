using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace CME.Localization.Mongo
{
    internal class LocalizedStringSerializer : SerializerBase<LocalizedString>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, LocalizedString value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(Dictionary<string, string>));
            serializer.Serialize(context, args, value);
        }

        public override LocalizedString Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.String)
            {
                var str = context.Reader.ReadString();
                return new LocalizedString(str);
            }
            else if (context.Reader.CurrentBsonType == BsonType.Document)
            {
                var serializer = BsonSerializer.LookupSerializer(typeof(BsonDocument));
                var document = serializer.Deserialize(context, args);
                var bsonDocument = document.ToBsonDocument();
                var result = BsonExtensionMethods.ToJson(bsonDocument);
                return JsonConvert.DeserializeObject<LocalizedString>(result);
            }
            context.Reader.ReadNull();
            return null;
        }
    }
}
