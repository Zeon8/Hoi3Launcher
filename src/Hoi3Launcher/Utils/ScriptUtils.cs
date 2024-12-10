namespace Hoi3Launcher.Utils
{
    public static class ScriptUtils
    {
        public static string ParseValue(string text, string key)
        {
            var startPosition = text.IndexOf(key);
            var startQuote = text.IndexOf('"', startPosition) + 1;
            var endQuote = text.IndexOf('"', startQuote);
            return text[startQuote..endQuote];
        }
    }
}
