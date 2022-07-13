# Httr Request

Class containing info about request sent to website
## Methods
### Constructors
```c#
HttrRequest request=new HttrRequest(selector, shouldretrieve);
```
creates new HttrRequest instance with one HttrTag
>Parameters:
>- [HttrSelector](./HttrSelector.md) ```selector``` - HttrSelector object containing  specifications for the retrieved tags 
>- <font color="DodgerBlue">bool</font> ```retrieveAttributes``` - bool indicating if the response should contain attributes

$~$


$~$

```c#
HttrRequest request=new HttrRequest(selectors, shouldretrieve);
```
creates new HttrRequest instance with multiple HttrTag
>Parameters:
>- <font color="DodgerBlue">List<[HttrSelector](./HttrSelector.md)></font> ```selectors``` - List of HttrSelector objects containing specifications for the retrieved tags 
>- <font color="DodgerBlue">bool</font> ```retrieveAttributes``` - bool indicating if the response should contain attributes
  
## Propreties

### Selectors
```c#
public List<HttrSelector> Selectors { get; set; }
```
List of HttrSelector objects that will set conditions for the retrieved tags
  
### RetrieveAttributes
```c#
public bool RetrieveAttributes { get; set; }
```
bool indicating if outputed json should contain attributes

### XPath
```c#
public string XPath { get; }
```
XPath to get all tags from html document 
