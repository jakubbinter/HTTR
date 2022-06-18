using HTTR;

var client = new HttrClient("https://www.gymceska.cz/");
var request = new HttrRequest(tagToRetrive: "div");
request.AddCondition("title", "Základní informace šk. roku 2021/2022");
//var client = new HttrClient("http://itcorp.com/");
//var request = new HttrRequest(tagToRetrive: "p");
var resp = client.SendRequest(request);

Console.WriteLine(resp);
