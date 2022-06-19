using Newtonsoft.Json.Linq;
using HtmlAgilityPack;

namespace HTTR.Test
{
    [TestClass]
    public class HttrClientTests:HttrClient
    {
        #region ParseHTML Tests
        [TestMethod]
        public void ParseHTML_EmptyString_EmptyJObject()
        {
            var ExpectedResult = new JObject();
            var doc=new HtmlDocument();
            doc.LoadHtml("");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes, new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }

        [TestMethod]
        public void ParseHTML_EmptyTag_JObjectWithTagAndEmptyJObject()
        {
            var ExpectedResult = new JObject(new JProperty("h1",new JArray()));
            var doc = new HtmlDocument();
            doc.LoadHtml("<h1></h1>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes, new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_SimpleHtml_SimpleJObject()
        {
            //var ExpectedResult = new JObject()["h1"] =new JArray(new JObject(
            //new JProperty("value",new JObject()["#text"]= new JArray("test"))));
            var ExpectedResult = JObject.Parse(@"
{
  ""h1"": [
    {
                ""value"": {
                    ""#text"": [
                      ""test""
                  ]
      }
            }
  ]
}"
);
            var doc = new HtmlDocument();
            doc.LoadHtml("<h1>test</h1>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes, new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_ComplexHtml_ComplexJObject()
        {
            var ExpectedResult = JObject.Parse(@"
{
  ""h1"": [
    {
                ""value"": {
                    ""#text"": [
                      ""test""
                  ]
      }
            }
  ],
  ""p"": [
    {
                ""value"": {
                    ""#text"": [
                      ""test p ""
                  ],
        ""a"": [
          {
                        ""value"": {
                            ""#text"": [
                              ""test a""
                          ]
            }
                    }
        ]
      }
            }
  ]
}"
);
            var doc = new HtmlDocument();
            doc.LoadHtml("<h1>test</h1><p>test p <a>test a</a></p>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes,new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        #endregion

        #region SendRequest Tests
        [TestMethod]
        public void SendRequest_SimpleRequest_ValidJson()
        {
            HttrClient client = new HttrClient("http://itcorp.com/");
            HttrRequest req = new HttrRequest(new HttrTag("h1"));
            var JObjectResult =  new JArray(new JObject(new JProperty("h1", new JArray(new JObject(new JProperty("value",
                new JObject(new JProperty("#text",new JArray("Interrupt Technology Corporation")))))))));
            string ExpectedResult = JObjectResult.ToString();
            var Result = client.SendRequest(req);
            Assert.AreEqual(ExpectedResult, Result);
        }
        #endregion
    }
}