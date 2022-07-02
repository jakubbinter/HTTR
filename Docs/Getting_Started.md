# Getting Started Guide
>to get started, first clone this repo, build it and then add reference to created dll to your project, where you want to use this library  

this code will return all divs that have class caled container from website https://github.com/jakubbinter/HTTR, the json will  contain all attribbutes

```c#
//create new client
var client = new HttrClient("https://github.com/jakubbinter/HTTR");
//Create new Httr Tag that will refer to html tag div
var tag = new HttrTag("p");
//Add comdition to the tag so it will only take div tags where attribute class = "f4 my-3"
tag.AddCondition("class", "f4 my-3");
//create new request with tag
var request=new HttrRequest(tag);
//send request and get json response
string jsonResponse=client.SendRequest(request);
```

if you want to exclude attributes from the response, change this line
```c#
var request=new HttrRequest(tag);
```
to this
```c#
var request=new HttrRequest(tag,false);
```
