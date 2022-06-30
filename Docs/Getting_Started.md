# Getting Started Guide
>to get started, first clone this repo, build it and then add reference to created dll to your project, where you want to use this library  

this code will return all divs that have class caled container from website randomurl.com

```c#
//create new client
var client=new HttrClient("https://randomurl.com");
//create new div tag taht has class container
var tag=new HttrTag("div");
tag.AddCondition("class","container");
//create new request with tag
var request=new HttrRequest(tag);
//send request and get json response
string jsonResponse=client.SendRequest(request);
```
