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
        protected HttrClient() { }

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
            doc.OptionOutputOriginalCase = true;
            var nodes = doc.DocumentNode.SelectNodes(request.XPath);
            JObject result = new JObject();

            //if nothing matches conditions return empty json
            if (nodes ==null)
                return new JObject().ToString();

            //parse html for all nodes
            result = ParseHTML(nodes,request);
            
            //return JObject as json string
            return result.ToString();
        }
        /// <summary>
        /// recursive method for transforming html to json
        /// </summary>
        /// <param name="nodes">htmlnodecollection of nodes you want to parse</param>
        /// <returns>JObject with inputed html transformed</returns>
        protected JObject ParseHTML(HtmlNodeCollection nodes,HttrRequest request)
        {
            var result = new JObject();
            List<string> elements=new List<string>();
            
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                string name = node.Name;
                JObject rs = null;
                //if there are child nodes then
                //  create new JObject with name of the tag and its index
                
                string indexedName = name + "$" + elements.Where(x => x == name).Count();
                if (node.HasChildNodes)
                {
                    result.Add(new JProperty(indexedName, new JObject()));
                    JObject res = result[indexedName] as JObject;
                    res.Add(new JProperty("value",ParseHTML(node.ChildNodes,request)));
                    rs = res;
                }
                //if the node is plain text
                //  create new JPropert with #text and its index and with value of the text
                //else
                //  create new empty JObject
                //  (if this happens the value is empty and we just want to have a JObject for atributes)
                else if(name[0]=='#')
                {
                    result.Add(new JProperty(indexedName, node.InnerHtml));                   
                }
                else
                {
                    result.Add(new JProperty(indexedName, new JObject()));
                    JObject res = result[indexedName] as JObject;
                    rs = res;
                }

                //add the tag name to list of elements
                elements.Add(name);
                //for each attribute in the node
                //  add its create new JProperty with name and index containing value of thet attribute
                //  add attribute name to the list of sttributes
                if (!request.RetrieveAttributes)
                    continue;
                List<string> attributes = new List<string>();
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    string attributeName = node.Attributes[j].Name + "$" + 
                        attributes.Where(x => x == node.Attributes[j].Name).Count();
                    rs.Add(new JProperty(attributeName, node.Attributes[j].Value));
                    attributes.Add(node.Attributes[j].Name);    
                }
            }
            return result;
        }
    }
}