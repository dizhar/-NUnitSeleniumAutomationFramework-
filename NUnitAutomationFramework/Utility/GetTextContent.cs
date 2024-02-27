using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace NUnitAutomationFramework.Utility
{
    public class GetTextContent
    {
    public string GetWikipediaSectionContent(string pageTitle, int sectionNumber)
{
    using (HttpClient client = new HttpClient())
    
    {

        string apiUrl = $"https://en.wikipedia.org/w/api.php?action=parse&page={pageTitle}&format=json&prop=sections|text&section={sectionNumber}";
        // Make the API request synchronously
        var response = client.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
          var json = response.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string into a dynamic object
        dynamic data = JsonConvert.DeserializeObject(json);

        // Access the content of the section from the parsed JSON
        string htmlString = data.parse.text["*"];


    
        // Parse the HTML string
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlString); // Replace htmlString with your HTML content
        
        // Select all elements with class "references" and remove them
        HtmlNodeCollection referenceNodes = doc.DocumentNode.SelectNodes("//*[@class='references']");
        if (referenceNodes != null)
        {
            foreach (HtmlNode referenceNode in referenceNodes)
            {
                referenceNode.Remove();
            }
        }

        // Select all div elements with role="note" and remove them
        HtmlNodeCollection noteNodes = doc.DocumentNode.SelectNodes("//*[@role='note']");
        if (noteNodes != null)
        {
            foreach (HtmlNode noteNode in noteNodes)
            {
                noteNode.ParentNode.RemoveChild(noteNode);
            }
        }


        // Extract text content
        string textContent = doc.DocumentNode.InnerText;

        string cleanedText = textContent.Replace("\n", " ");

        return cleanedText;
        }
        else
        {
            return "Error: " + response.StatusCode;
        }
    }
}
       
    }
}
