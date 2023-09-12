using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk.Data.Extensions
{
    public static class ParameterCollectionExtensions
    {
        public static void SetValue<T>(this ParameterCollection collection, string key, T value)
        {
            _ = collection ?? throw new ArgumentNullException(nameof(collection));
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (collection.ContainsKey(key))
                collection[key] = value;
            else
                collection.Add(key, value);
        }

        public static T GetValue<T>(this ParameterCollection collection, string key)
        {
            _ = collection ?? throw new ArgumentNullException(nameof(collection));
            _ = key ?? throw new ArgumentNullException(nameof(key));

            if (collection.Contains(key))
                return (T)collection[key];
            else
                return default;
        }
    }
}
