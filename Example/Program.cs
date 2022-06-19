using HTTR;

var client = new HttrClient("https://www.gymceska.cz/");
var mtag = new HttrTag("div");
mtag.AddCondition("title", "Základní informace šk. roku 2021/2022");
var stag=new HttrTag("img",true,new string[] { "src"});

var request = new HttrRequest(new List<HttrTag> { mtag ,stag});

/*var client = new HttrClient("http://itcorp.com/");
var request = new HttrRequest(new HttrTag("p"));*/
var resp = client.SendRequest(request);

Console.WriteLine(resp);
