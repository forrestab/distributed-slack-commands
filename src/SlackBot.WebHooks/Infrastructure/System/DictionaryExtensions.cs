using System;
using System.Collections.Generic;

namespace SlackBot.WebHooks.Infrastructure.System
{
    public static class DictionaryExtensions
    {
        public static void AddIf<TKey, TValue>(this IDictionary<TKey, TValue> items, Func<TValue, bool> condition, TKey key, TValue value)
        {
            if (condition(value))
            {
                items.Add(key, value);
            }
        }
    }
}
