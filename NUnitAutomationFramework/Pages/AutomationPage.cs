using OpenQA.Selenium;
using NUnitAutomationFramework.WebElements;
using NUnitAutomationFramework.Base;
using AventStack.ExtentReports;
using System.Collections.ObjectModel;
using NUnitAutomationFramework.Utility;
using NUnit.Framework;

namespace NUnitAutomationFramework.Pages
{
    public class AutomationPage
    {
        private readonly IWebDriver driver;
        private readonly ExtentTest test;
        public AutomationPage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
        }


        //*[@id="Methodologies"]
        readonly string MethodologiesSection = "//*[@id='Methodologies']";

        public  Dictionary<string, int>  getAllSectionWordDictionaryValues()
        {
            // Now you can use the dynamically constructed section selector
            IWebElement titleElement = ActionsElements.FindElement(driver, By.XPath(MethodologiesSection));
            int numH3Elements = CountNumberOSiblingsWithinASection("h3");
            int numPElements = CountNumberOSiblingsWithinASection("p");
      
            List<IWebElement> h3Elements = getListOfSectionsElements(numH3Elements, "h3");
            List<IWebElement> pElements = getListOfSectionsElements(numPElements, "p");
            List<string> titles = getAListOfStrings(h3Elements);
            List<string> paragraphs = getAListOfStrings(pElements);
            List<string> combinedList = getCombinedTitlesAndParagraphs(titles, paragraphs);

            // Combine all elements of the combined list into a single string separated by a delimiter
            string combinedString = string.Join(Environment.NewLine, combinedList);
            GetUniqueWord textUniqueWord = new GetUniqueWord();
            Dictionary<string, int> dict1 = textUniqueWord.GetUniqueWordCounts(combinedString);
            return dict1;     
        }

        private  List<string> getCombinedTitlesAndParagraphs (List<string> titles, List<string> paragraphs)
        {
            // Combine titles and paragraphs into a single list
            List<string> combinedList = new List<string>();
             IWebElement titleElement = ActionsElements.FindElement(driver, By.XPath(MethodologiesSection));
             string sectionTitle = titleElement.FindElement(By.XPath("..")).Text;
            combinedList.Add(sectionTitle);
            combinedList.AddRange(titles);
            combinedList.AddRange(paragraphs);
            return combinedList;
        }

        private static List<string> getAListOfStrings(List<IWebElement> elements)
        {
            List<string> titles = new List<string>();
            foreach (IWebElement element in elements)
            {
                titles.Add(element.Text);
            }

            return titles;
        }

        private List<IWebElement> getListOfSectionsElements(int numElements, string el)
        {
            IWebElement titleElement = ActionsElements.FindElement(driver, By.XPath(MethodologiesSection));
            IWebElement sectionElement = titleElement.FindElement(By.XPath(".."));
            List<IWebElement> listOfElements = new List<IWebElement>();
            for (int x = 0; x < numElements; x++)
            {
                listOfElements.Add(sectionElement.FindElement(By.XPath($"following-sibling::{el}[{x+1}]")));
            }

            return listOfElements;
        }

        private int CountNumberOSiblingsWithinASection(string el)
        {
            IWebElement titleElement = ActionsElements.FindElement(driver, By.XPath(MethodologiesSection));
            IWebElement sectionElement = titleElement.FindElement(By.XPath(".."));
            List<IWebElement> h3Elements = new List<IWebElement>();

            // Find all the following siblings of the h2 element
            ReadOnlyCollection<IWebElement> siblings = sectionElement.FindElements(By.XPath("following-sibling::*"));

            // Iterate through the siblings
            foreach (var sibling in siblings)
            {
                if (sibling.TagName.Equals("h2") && !sibling.Equals(sectionElement))
                {
                    // If a next h2 element is encountered, break the loop
                    break;
                }
                else if (sibling.TagName.Equals(el))
                {
                    // If the sibling is an h3 element, add it to the list
                    h3Elements.Add(sibling);
                }
            }

            return h3Elements.Count;
        }
    }
}


