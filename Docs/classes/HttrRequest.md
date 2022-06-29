# Httr Request

Class containing info about request sent to website
## Methods
### Constructors
```c#
HttrRequest request=new HttrRequest(tagToRetrive);
```
creates new HttrRequest instance with one HttrTag
>Parameters:
>- [HttrTag](./HttrTag.md)``` tagToRetrive``` - HttrTag object containing specifications of the tag you want to get 

$~$


$~$

```c#
HttrRequest request=new HttrRequest(tagsToRetrive);
```
creates new HttrRequest instance with multiple HttrTag
>Parameters:
>- <font color="DodgerBlue">List<[HttrTag](./HttrTag.md)></font>``` tagsToRetrive``` - List of HttrTag objects containing specifications of tags you want to get 

## Propreties

### TagsToRetrive
```c#
public List<HttrTag> TagsToRetrive { get; set; }
```
List of HttrTag objects that will be retrieved from the website

### XPath
```c#
public string XPath { get; }
```
XPath to get all tags from html document 