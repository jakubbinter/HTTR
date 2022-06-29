using HTTR;

var client = new HttrClient("https://www.csfd.cz/film/4324-srdce-v-atlantide/prehled/");
var mtag = new HttrTag("img", attributesToRetrieve: new string[]{"src"});



var request = new HttrRequest(new List<HttrTag> { mtag});

/*var client = new HttrClient("http://itcorp.com/");
var request = new HttrRequest(new HttrTag("p"));*/
var resp = client.SendRequest(request);

Console.WriteLine(resp);
