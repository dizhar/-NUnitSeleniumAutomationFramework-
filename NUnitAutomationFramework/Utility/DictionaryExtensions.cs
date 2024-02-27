using System.Collections.Generic;

public class DictionaryExtensions
{
    public  bool AreDictionariesEqual<TKey, TValue>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
    {
        // Check if dictionaries have the same count
        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        // Check if all key-value pairs in dict1 are present in dict2
        foreach (KeyValuePair<TKey, TValue> kvp in dict1)
        {
            if (!dict2.TryGetValue(kvp.Key, out TValue value) || !EqualityComparer<TValue>.Default.Equals(kvp.Value, value))
            {
                return false;
            }
        }

        // Check if all key-value pairs in dict2 are present in dict1 (optional if you want to ensure bidirectional equality)
        //foreach (KeyValuePair<TKey, TValue> kvp in dict2)
        //{
        //    if (!dict1.TryGetValue(kvp.Key, out TValue value) || !EqualityComparer<TValue>.Default.Equals(kvp.Value, value))
        //    {
        //        return false;
        //    }
        //}

        return true;
    }
}
