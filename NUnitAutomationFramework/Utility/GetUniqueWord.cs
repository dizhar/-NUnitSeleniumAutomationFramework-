using System.Xml;
using System.Text.RegularExpressions;

namespace NUnitAutomationFramework.Utility
{
    public class GetUniqueWord
    {
          public Dictionary<string, int> GetUniqueWordCounts(string text)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        // Remove brackets and their contents
        text = Regex.Replace(text, @"\[[^\]]*\]", "");

        // Replace delimiters with space
        char[] delimiters = { '.', ',', '-', '!', '?', ':', ';', ' ' };
        foreach (char delimiter in delimiters)
        {
            text = text.Replace(delimiter, ' ');
        }

        // Split text into words
        string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Convert to lowercase and count occurrences
        foreach (string word in words)
        {
            // Replace "4&#93;" and consecutive whitespace with a single space
            string cleanWord = Regex.Replace(word.ToLower(), @"(4&#93;|\s+)", " ");

            // Skip empty strings
            if (string.IsNullOrWhiteSpace(cleanWord))
            {
                continue;
            }

            // Split the cleaned word into individual words
            string[] individualWords = cleanWord.Split(' ');

            // Iterate through individual words and count them
            foreach (string individualWord in individualWords)
            {
                if (!wordCounts.ContainsKey(individualWord))
                {
                    wordCounts[individualWord] = 1;
                }
                else
                {
                    wordCounts[individualWord]++;
                }
            }
        }

        // Remove words matching a regular expression pattern
        string patternToRemove = @"[,.\d&]+"; // Pattern to match commas, dots, digits, and ampersands
        Regex regex = new Regex(patternToRemove);
        List<string> keysToRemove = new List<string>();
        foreach (string word in wordCounts.Keys)
        {
            if (regex.IsMatch(word))
            {
                keysToRemove.Add(word);
            }
        }
        foreach (string keyToRemove in keysToRemove)
        {
            wordCounts.Remove(keyToRemove);
        }

        return wordCounts;
    }
    }
}
    
    
     


