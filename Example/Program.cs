using HTTR;

//Create Httr Client with url of the site you want to get data from
var client = new HttrClient("https://github.com/jakubbinter/HTTR");
//Create new Httr Tag that will refer to html tag div
var mtag = new HttrTag("p");
//Add comdition to the tag so it will only take div tags where attribute class = "f4 my-3"
mtag.AddCondition("class", "f4 my-3");
//Create Httr Request, as parametr pass all tag you created
var request = new HttrRequest(mtag);

//Send request - your response wil be json string
var resp = client.SendRequest(request);

Console.WriteLine(resp);
