using HtmlAgilityPack;

namespace HTTR
{
    public class HttrClient
    {
        public string Url { get; set; }
        public HttrClient(string url)
        {
            Url = url;
        }
        public List<string> SendRequest(HttrRequest request)
        {
            var web = new HtmlWeb();
            var doc = web.Load(Url);
            
            var nds = doc.DocumentNode.SelectNodes(request.TagToRetrive + request.Conditions);
            List<string> result = new List<string>();
            if(nds ==null)
                return result;
            HtmlNode[] nodes =nds.ToArray();
            
            for (int i = 0; i < nodes.Length; i++)
            {
                result.Add(nodes[i].Attributes[request.AtributeToRetrive].Value);
            }
            return result;
        }

    }
}