using Newtonsoft.Json.Linq;

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
            var Result = ParseHTML("");
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }

        [TestMethod]
        public void ParseHTML_EmptyTag_JObjectWithTagAndEmptyJObject()
        {
            var ExpectedResult = new JObject(new JProperty("h1",""));
            var Result = ParseHTML("<h1></h1>");
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_SimpleHtml_SimpleJObject()
        {
            var ExpectedResult = new JObject(new JProperty("h1", "test"));
            var Result = ParseHTML("<h1>test</h1>");
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        [TestMethod]
        public void ParseHTML_ComplexHtml_ComplexJObject()
        {
            var ExpectedResult = new JObject(
                new JProperty("h1", "test"),
                new JProperty(
                    new JProperty("p",new JObject(
                        new JProperty("#text","test p "),
                        new JProperty("a","test a")))));
            var Result = ParseHTML("<h1>test</h1><p>test p <a>test a</a></p>");
            Assert.AreEqual(ExpectedResult.ToString(), Result.ToString());
        }
        #endregion

        #region SendRequest Tests
        [TestMethod]
        public void SendRequest_SimpleRequest_ValidJson()
        {
            HttrClient client = new HttrClient("http://itcorp.com/");
            HttrRequest req = new HttrRequest("h1");
            var JObjectResult = new JObject();
            JObjectResult["items"] = new JArray(new JObject(new JProperty("h1", "Interrupt Technology Corporation")));
            string ExpectedResult = JObjectResult.ToString();
            var Result = client.SendRequest(req);
            Assert.AreEqual(ExpectedResult, Result);
        }
        #endregion
    }
}