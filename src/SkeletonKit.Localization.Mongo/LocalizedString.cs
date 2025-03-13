namespace CME.Localization.Mongo
{
    /// <summary>
    /// Represents a string that can be localized. 
    /// Should be used in entity classes that represent a MongoDB document.
    /// </summary>
    public sealed class LocalizedString : Dictionary<string, string>
    {
        public LocalizedString() : base()
        {
        }

        public LocalizedString(string locale) : base()
        {
            this.Add(DefaultLocale, locale);
        }

        public string Default => this.GetValueOrDefault(DefaultLocale);

        public string GetFromLocale(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale)) return Default;
            if (TryGetValue(locale, out var res)) return res;
            var langCountry = locale.Split('-');
            if (langCountry.Length > 1)
            {
                var lang = langCountry[0];
                if (TryGetValue(lang, out var res2)) return res2;
            }
            return Default;
        }

        public const string DefaultLocale = "en";
    }
}
