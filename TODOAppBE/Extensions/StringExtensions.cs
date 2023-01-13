namespace TODOAppBE.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }
}
