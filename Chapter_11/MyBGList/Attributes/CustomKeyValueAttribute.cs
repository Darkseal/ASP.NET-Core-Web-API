namespace MyBGList.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
    public class CustomKeyValueAttribute : Attribute
    {
        public CustomKeyValueAttribute(string? key, string? value)
        {
            Key = key;
            Value = value;
        }

        public string? Key { get; set; }

        public string? Value { get; set; }
    }
}
