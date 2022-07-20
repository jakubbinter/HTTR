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
        /// Method for sending request and returning their value as JSON
        /// </summary>
        /// <param name="request">HttrRequest providing info about your request</param>
        /// <returns>returns string in json format with html tags transformed to json objects</returns>
        public string SendRequest(HttrRequest request)
        {
            //initilazing basic variables
            HttpClient client = new HttpClient();
            var response = client.GetAsync(Url).Result;
            HttrParser parser = new HttrParser(response.Content.ReadAsStringAsync().Result);
            return parser.GetJsonFromString(request);
            
        }
        
    }
}