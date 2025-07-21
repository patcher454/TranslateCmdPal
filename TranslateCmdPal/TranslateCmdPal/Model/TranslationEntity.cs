using System;
using System.Text.Json.Serialization;

namespace TranslateCmdPal.Model
{
    public class TranslationEntity
    {
        [JsonPropertyName(nameof(OriginalText))]
        public string OriginalText { get; set; }

        [JsonPropertyName(nameof(TranslatedText))]
        public string TranslatedText { get; set; }

        [JsonPropertyName(nameof(OriginalLangCode))]
        public string OriginalLangCode { get; set; }

        [JsonPropertyName(nameof(TargetLangCode))]
        public string TargetLangCode { get; set; }

        [JsonPropertyName(nameof(Timestamp))]
        public DateTime Timestamp { get; set; }
    }
}
