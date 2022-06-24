using HTTR;

//Create Httr Client with url of the site you want to get data from
var client = new HttrClient("https://www.gymceska.cz/");
//Create new Httr Tag that will refer to html tag div
var mtag = new HttrTag("div");
//Add comdition to the tag so it will only take div tags where attribute title ="Základní informace šk. roku 2021/2022"
mtag.AddCondition("title", "Základní informace šk. roku 2021/2022");
//Create another tag that will not be retrived from the site-it will only set conditions for attributes if this tag is encountred inside of some retrieved tag
var stag=new HttrTag("img",true,new string[] { "src"});

//Create Httr Request, as parametr pass all tags you created
var request = new HttrRequest(new List<HttrTag> { mtag ,stag});

//Send request - your response wil be json string
var resp = client.SendRequest(request);

Console.WriteLine(resp);
