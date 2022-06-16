using HtmlAgilityPack;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace HTTR
{
    public class HttrClient
    {
        public string Url { get; set; }
        public HttrClient(string url)
        {
            Url = url;
        }
        public string SendRequest(HttrRequest request)
        {
            var web = new HtmlWeb();
            var doc = web.Load(Url);
            
            var nds = doc.DocumentNode.SelectNodes(request.TagToRetrive + request.Conditions);
            JArray retrieved = new JArray();
            if(nds ==null)
                return new JObject().ToString();
            HtmlNode[] nodes =nds.ToArray();
            
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
            JObject result = new JObject();
            result["items"] = retrieved;
            return result.ToString();
            
        }

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