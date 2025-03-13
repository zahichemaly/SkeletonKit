using MongoDB.Bson.Serialization;
using System.Linq.Expressions;

namespace CME.Localization.Mongo
{
    public static class LocalizedStringInitializer
    {
        public static BsonMemberMap MapLocalizedStringField<TClass, TMember>(
            this BsonClassMap<TClass> cm,
            Expression<Func<TClass, TMember>> fieldLambda)
        {
            return cm.MapField(fieldLambda).SetSerializer(new LocalizedStringSerializer());
        }
    }
}
