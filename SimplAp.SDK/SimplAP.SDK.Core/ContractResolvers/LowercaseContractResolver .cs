using Newtonsoft.Json.Serialization;

namespace SimplAP.SDK.Core.ContractResolvers
{
    internal class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            // Check if the string is empty or already starts with a lowercase letter
            if (string.IsNullOrEmpty(propertyName) || char.IsLower(propertyName, 0))
            {
                return propertyName;
            }

            // Convert the first letter to lowercase
            return char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}
