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
                    obj[nodes[i].OriginalName] = nodes[i].InnerHtml;
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

        
    }
}