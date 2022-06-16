using HtmlAgilityPack;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace HTTR
{
    public class HttrClient
    {
        public string Url { get; set; }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="url">url of web page from which you want to get info from</param>
        public HttrClient(string url)
        {
            Url = url;
        }

        /// <summary>
        /// Main method for sending request and returning their value as json
        /// </summary>
        /// <param name="request">HttrRequest providing info about your request</param>
        /// <returns>returns string in json format withhtml tags transformed to json object</returns>
        public string SendRequest(HttrRequest request)
        {
            //initilazing basic variables
            var web = new HtmlWeb();
            var doc = web.Load(Url);
            var nds = doc.DocumentNode.SelectNodes(request.TagToRetrive + request.Conditions);
            JArray retrieved = new JArray();

            //if nothing matches conditions return empty json
            if (nds ==null)
                return new JObject().ToString();

            //get all nodes in array
            HtmlNode[] nodes =nds.ToArray();
            
            //for each node
            //if the atribute they want is value
            //  then parse the inner html of the node
            //  and add it in json format to JArray
            //else 
            //  just add the atribute they want to the JArray
            for (int i = 0; i < nodes.Length; i++)
            {
                var obj=new JObject();
                if (request.AtributeToRetrive == "value")
                {
                    obj[nodes[i].OriginalName] = ParseHTML(nodes[i].InnerHtml);
                    retrieved.Add(obj);
                }
                else
                {
                    obj[nodes[i].OriginalName] = nodes[i].Attributes[request.AtributeToRetrive].Value;
                    retrieved.Add(obj);
                }
            }

            //add the JArray to the main JObject, construct json string and return this string
            JObject result = new JObject();
            result["items"] = retrieved;
            return result.ToString();
        }
        /// <summary>
        /// recursive method for transforming html to json
        /// </summary>
        /// <param name="html">html string</param>
        /// <returns>JToken(either string or JObject) with inputed html transformed</returns>
        JToken ParseHTML(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var result = new JObject();
            for (int i = 0; i < doc.DocumentNode.ChildNodes.Count; i++)
            {
                var node = doc.DocumentNode.ChildNodes[i];
                string name = node.OriginalName;
                if (node.HasChildNodes)
                {
                    result[name] = ParseHTML(node.InnerHtml);         
                }
                else if(doc.DocumentNode.ChildNodes.Count==1)
                {                  
                    return node.InnerHtml;
                }
            }
            return result;
        }
    }
}