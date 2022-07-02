# Getting Started Guide
>to get started, download Build folder from this repo and then in your project add reference to HTTR.dll  

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

in the first case the json response will look like this
```json
{
  "p$0": {
    "value": {
      "#text$0": "\n      c# library for sending request to html websites and getting rest-like response in json format\n    "
    },
    "class$0": "f4 my-3"
  }
}
```
in the second case it will look like this
```json
{
  "p$0": {
    "value": {
      "#text$0": "\n      c# library for sending request to html websites and getting rest-like response in json format\n    "
    }
  }
}
```
The json consist of one or more properties, which name consist of html tag name, "$" sign and its index - the index is basicly a number how many times this tag occurred in the parent object of this property.
The value of this property consist of "value" object (if there is value to that object-for example img tags won't have any value), which contains one or more tags with syntax described before.
If the value of the property is text then the tag will be called #text, the same thing with comments - it will be #comment and their valu wont be object but only string.
If returning attributes is enabled (you can set that in HttrRequest constructor), there will be also these attributes contained in the object, they will have the same syntax as tags - name of the attribute + "$" sign + its index, value of these properties is string containing value of the html attribute.
