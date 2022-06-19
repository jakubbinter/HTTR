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
            var nodes = doc.DocumentNode.SelectNodes(request.TagToRetrive + request.Conditions);
            JArray result = new JArray();

            //if nothing matches conditions return empty json
            if (nodes ==null)
                return new JObject().ToString();

            //for each node
            //  create html document with that node and take node collection and call parseHTML
            for (int i = 0; i < nodes.Count; i++)
            {
                var dc = new HtmlDocument();
                dc.LoadHtml(nodes[i].OuterHtml);
                var col = dc.DocumentNode.ChildNodes;
                var obj = ParseHTML(col);
                result.Add(obj);
            }

            //return JArray as json string
            return result.ToString();
        }
        /// <summary>
        /// recursive method for transforming html to json
        /// </summary>
        /// <param name="nodes">htmlnodecollection of nodes you want to parse</param>
        /// <returns>JObject with inputed html transformed</returns>
        protected JObject ParseHTML(HtmlNodeCollection nodes)
        {
            var result = new JObject();
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                string name = node.Name;

                //if there are child nodes then
                //  either create new array and add Jobject with Jproperty containing values
                //  or add to existing array
                if (node.HasChildNodes)
                {
                    if (result.ContainsKey(name))
                    {
                        (result[name] as JArray).Add(new JObject(new JProperty("value",ParseHTML(node.ChildNodes))));
                    }
                    else
                    {
                        result[name] = new JArray(new JObject(new JProperty("value", ParseHTML(node.ChildNodes)))); 
                    }          
                }
                //if the node is plain text
                //  add it to JArray
                //else
                //  create new empty JArray
                //  (if this happens the value is empty and we just want to have a JArray for atributes)
                else
                {
                    if (name == "#text")
                    {
                        if (result.ContainsKey(name))
                        {
                            (result[name] as JArray).Add(node.InnerHtml);
                        }
                        else
                        {
                            result[name] = new JArray(node.InnerHtml);
                        }
                    }
                    else
                    {
                        if (!result.ContainsKey(name))
                        {
                            result[name] = new JArray();
                        }
                    }
                }

                //for each attribute in the node
                //  if there is empty array
                //      add new JObject with JProperty of the html tag
                //  else 
                //      if there is already JProperty with name of this attribute
                //          than add value of this to it to it
                //      else
                //          add new JProperty containing this attribute
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    var arr = result[node.Name] as JArray;
                    if (arr.Count == 0)
                    {
                        arr.Add(new JObject(new JProperty(node.Attributes[j].Name, node.Attributes[j].Value)));
                        continue;
                    }
                    bool cont = (arr[arr.Count - 1] as JObject).ContainsKey(node.Attributes[j].Name);
                    if (cont)
                        (arr[arr.Count - 1] as JObject)[node.Attributes[j].Name] += node.Attributes[j].Value;
                    else
                    {
                        (arr[arr.Count - 1] as JObject).Add(new JProperty(node.Attributes[j].Name, node.Attributes[j].Value));  
                    }
                        
                }
            }
            return result;
        }
    }
}