using TranslateCmdPal.Enum;


namespace TranslateCmdPal.Util
{
    public class InputInterpreter
    {
        public static (LangCode.Code, string) Parse(string query, LangCode.Code defaultLangCode)
        {
            if (string.IsNullOrWhiteSpace(query))
                return (LangCode.Code.UNK, string.Empty);

            var parts = query.Split(new[] { ' ' }, 2);

            if (parts.Length == 2)
            {
                string targetLangCode = parts[0];
                string text = parts[1];

                if (!string.IsNullOrEmpty(targetLangCode) && !string.IsNullOrEmpty(text))
                {
                    var target = LangCode.Parse(targetLangCode);
                    if (target == LangCode.Code.UNK)
                    {
                        return (defaultLangCode, query);
                    }
                    return (target, text);
                }
            }

            if (parts.Length == 1)
            {
                string text = parts[0];
                if (!string.IsNullOrEmpty(text))
                {
                    return (defaultLangCode, text);
                }
            }

            return (LangCode.Code.UNK, string.Empty);
        }
    }
}
