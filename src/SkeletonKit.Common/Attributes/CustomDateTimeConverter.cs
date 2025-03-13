using Newtonsoft.Json.Converters;

namespace SkeletonKit.Common.Attributes
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssZ";
        }

        public CustomDateTimeConverter(string format)
        {
            base.DateTimeFormat = format;
        }
    }
}
