namespace Medyana.Common.Helpers.Shared
{
    public static class StringHelper
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotEmpty(this string text)
        {
            return !IsEmpty(text);
        }
    }
}