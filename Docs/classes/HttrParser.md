# Httr Parser

Class containing info about request sent to website
## Methods
### Constructors
```c#
HttrParser parser=new HttrParser(text);
```
creates new HttrParser instance with html text to parse
>Parameters:
>- <font color="DodgerBlue">string</font> ```text``` - html string to parse

### GetJsonFromString

```c#
parser.GetJsonFromString(request) 
```
parses the html string and returns json output

>Parameters:
>- [HttrRequest](./HttrRequest.md) ```request``` - request with information about tags you want to retrieve

## Propreties

### XPath
```c#
public string Html { get; set; }
```
Html string to parse
