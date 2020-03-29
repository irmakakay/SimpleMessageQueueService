namespace MessageQueue.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToFileName(this string name, string extension) => 
            string.Join('.', name, extension);

        public static string FormatFileName(this string name, string section, string extension) =>
            $"{name}.{section}".ToFileName(extension);
    }
}
