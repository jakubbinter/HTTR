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
            /*TODO: vracet JObject i se všema atributama který má
              PROBLEM: musim vymyslet jak upravit httrRequest aby to dávalo smysl
            {
              "items":
                [
                    { 
                        "h1":
                        {
                            "id":"some id"
                            "class":"some class"
                            "value":
                            {
                                "#text":"nějakej nadpis"
                                "a":
                                {
                                    "href":"https://randomshit.com"
                                    "value":
                                    {   
                                        "#text":"random odkaz"
                                    }
                                }
                            }
                        }
                    }   
                ]
            }
            */

            //initilazing basic variables
            var web = new HtmlWeb();
            var doc = web.Load(Url);
            doc.OptionOutputOriginalCase = true;
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
                obj[nodes[i].Name] = ParseHTML(nodes[i].ChildNodes);
                retrieved.Add(obj);
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
        protected JToken ParseHTML(HtmlNodeCollection nodes)
        {
            var result = new JObject();
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                string name = node.Name;
                if (node.HasChildNodes)
                {
                    if (result.ContainsKey(name))
                    {
                        (result[name] as JArray).Add(new JArray(ParseHTML(node.ChildNodes)));
                    }
                    else
                    {
                        result[name] = new JArray();
                        (result[name] as JArray).Add(new JArray(ParseHTML(node.ChildNodes)));
                    }
                             
                }
                else
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
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    var arr = result[node.Name] as JArray;
                    int index = arr.IndexOf(node.Attributes[j].Name);
                    if (index != -1)
                        arr[index][node.Attributes[j].Name] += node.Attributes[j].Value;
                    else
                        arr.Add(new JObject(new JProperty(node.Attributes[j].Name, node.Attributes[j].Value)));
                }
            }
            return result;
        }
    }
}