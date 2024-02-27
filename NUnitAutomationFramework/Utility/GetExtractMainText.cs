using System.Xml;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace NUnitAutomationFramework.Utility
{
    public class GetExtractMainText
    {
    
        public string extractMainText(string json)
        {
            string extract = (string)JObject.Parse(json)["query"]["pages"][0]["extract"];

            // Load the extract HTML into an HtmlDocument
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(extract);

            // Select all elements except for <sup> tags (references)
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[not(self::sup)]");

            // Concatenate the inner text of all selected nodes
            string mainText = "";
            foreach (HtmlNode node in nodes)
            {
                mainText += node.InnerText;
            }

            return mainText;
        }
       
    }
}
