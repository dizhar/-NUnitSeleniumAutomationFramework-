using NUnit.Framework;
using NUnitAutomationFramework.Base;
using NUnitAutomationFramework.Pages;
using NUnitAutomationFramework.Utility;


namespace NUnitAutomationFramework.TestSuites
{
    [Parallelizable(ParallelScope.Children)]
    [Description("Verify Unique Word Counts for Wikipedia Section")]
    public class Regression : BaseSetup
    {
        [Test, Category("Regression")]
        public void TC001()
        { 
           //Test Case:
            //1. Redirect to the page https://en.wikipedia.org/wiki/Test_automation.
            //2.Scroll to the Methodology section.
            //3.Count all the unique words and their frequencies.
            //4. Perform a Wikipedia API request: GET https://en.wikipedia.org/w/api.php?action=parse&page=Test_automation&format=json&prop=sections|text&section=7.
            //5.Read the JSON response and exclude the references.
            //6.Count all the unique words and their frequencies.
            //7. Verify that the counts from steps 3 and 6 are equal.


            // Test Title: Verify Unique Word Counts for Wikipedia Section
            string? testcase = TestContext.CurrentContext.Test.MethodName;
            string testdata = ReadTestData.GetTestData(testcase, "TestData");
            extent_test.Value.Info("Testdata is : " +testdata);

            string user = ReadUsers.UserList("Registered_User");
            extent_test.Value.Info("Testdata is : " + user);
         
            AutomationPage page = new(GetDriver(), extent_test.Value);
            string sectionName = "Methodologies";
            // Get the section unique Word Count via selenium
            Dictionary<string, int> dict1 = page.getAllSectionWordDictionaryValues();


            string pageTitle = "Test_automation";
            int sectionNumber = 7;
            GetTextContent textContentGetter = new GetTextContent();
            GetUniqueWord textUniqueWord = new GetUniqueWord();
            DictionaryExtensions dictionaryExtensions = new DictionaryExtensions();
     

            // Get the section unique Word Count via api
            string sectionString = textContentGetter.GetWikipediaSectionContent(pageTitle, sectionNumber);
            Dictionary<string, int> dict2 = textUniqueWord.GetUniqueWordCounts(sectionString);
            // Convert the dictionary to a string representation
            string dictString1 = "{ " + string.Join(", ", dict1.Select(kvp => $"{kvp.Key}: {kvp.Value}")) + " }";
            string dictString2 = "{ " + string.Join(", ", dict2.Select(kvp => $"{kvp.Key}: {kvp.Value}")) + " }";

            // Print the string representation
            Console.WriteLine(dictString1);

            Console.WriteLine(dictString2);
            dictionaryExtensions.AreDictionariesEqual(dict1, dict2);
        }
    }
}




