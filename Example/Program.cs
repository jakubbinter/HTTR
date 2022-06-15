using HTTR;

var client = new HttrClient("https://www.w3schools.com/whatis/whatis_json.asp");
var request = new HttrRequest(tagToRetrive:"img",atributeToRetrieve:"src");
var resp = client.SendRequest(request);

foreach (var item in resp)
{
    Console.WriteLine(item);
}