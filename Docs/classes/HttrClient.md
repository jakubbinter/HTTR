# Httr Client

Main class for sending requests  
## Methods
### Constructor
```c#
HttrClient client=new HttrClient("https://ranodmurl.com");
```
creates new HttrClient instance  
>Parameters:
>- <font color="DodgerBlue">string</font>``` url``` - url of website you want to get data from

### SendRequest
```c#
client.SendRequest(request);
```
sends request to website  
>Parameters:
>- [HttrRequest](./HttrRequest.md)``` request``` - request with information about tags you want to retrieve

## Propreties

### Url
```c#
public string Url { get; set; }
```
Url of requested website