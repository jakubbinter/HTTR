using Newtonsoft.Json.Linq;
using HtmlAgilityPack;

namespace HTTR.Test
{
    [TestClass]
    public class HttrClientTests:HttrParser
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
            var ExpectedResult = new JObject(new JProperty("h1$0",new JObject()));
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
            var ExpectedResult = new JObject(new JProperty("h1$0",new JObject(
                new JProperty("value",
                    new JObject(
                        new JProperty("#text$0","test"))))),new JProperty("h1$1", new JObject(
                new JProperty("value",
                    new JObject(
                        new JProperty("#text$0", "test2"))))));

            var doc = new HtmlDocument();
            doc.LoadHtml("<h1>test</h1><h1>test2</h1>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes, new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_ComplexHtml_ComplexJObject()
        {
            var ExpectedResult = JObject.Parse(@"
{
  ""h1$0"": {
    ""value"": {
                ""#text$0"": ""test""
    }
        },
  ""p$0"": {
    ""value"": {
      ""#text$0"": ""test p "",
      ""a$0"": {
        ""value"": {
          ""#text$0"": ""test a""
        }
      }
    }
  }
}"
);
            var doc = new HtmlDocument();
            doc.LoadHtml("<h1>test</h1><p>test p <a>test a</a></p>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes,new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_SimpleHtmlWithAttributes_SimpleJObject()
        {
            var ExpectedResult = new JObject(new JProperty("h1$0", new JObject(
                new JProperty("value",
                    new JObject(
                        new JProperty("#text$0", "test"))), 
                        new JProperty("class$0", "test"),
                        new JProperty("class$1", "test2"))));

            var doc = new HtmlDocument();
            doc.LoadHtml("<h1 class=\"test\" class=\"test2\">test</h1>");
            var Result = ParseHTML(doc.DocumentNode.ChildNodes, new HttrRequest(new HttrTag("div")));
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        #endregion

        #region SendRequest Tests
        [TestMethod]
        public void SendRequest_SimpleRequest_ValidJson()
        {
            HttrClient client = new HttrClient("http://itcorp.com/");
            HttrRequest req = new HttrRequest(new HttrTag("h1"));
            var JObjectResult =  new JObject(new JProperty("h1$0", new JObject(new JProperty("value",
                new JObject(new JProperty("#text$0","Interrupt Technology Corporation"))))));
            string ExpectedResult = JObjectResult.ToString();
            var Result = client.SendRequest(req);
            Assert.AreEqual(ExpectedResult, Result);
        }
        #endregion

        #region GetJsonFromString Tests
        public void GetJsonFromString_SimpleRequest_ValidJson()
        {
            HttrParser parser = new HttrParser("<h1>Interrupt Technology Corporation</h1>");
            HttrRequest req = new HttrRequest(new HttrTag("h1"));
            var JObjectResult = new JObject(new JProperty("h1$0", new JObject(new JProperty("value",
                new JObject(new JProperty("#text$0", "Interrupt Technology Corporation"))))));
            string ExpectedResult = JObjectResult.ToString();
            var Result = parser.GetJsonFromString(req);
            Assert.AreEqual(ExpectedResult, Result);
        }
        #endregion
    }
}