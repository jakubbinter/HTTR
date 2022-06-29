# Httr Tag
Class representing Html tag
## Methods
### Constructor
```c#
HttrTag tag=new HttrTag(tagToRetrive, isInnerTag, attributesToRetrieve)
```
creates new HttrTag instance  
>Parameters:
>- <font color="DodgerBlue">string</font>``` tagToRetrive``` - name of html tag this class will represent
>- <font color="DodgerBlue">bool</font>``` isInnerTag``` - bool indicating if this tag will be retrieved from html document or if it will only indicate what attributes to retrive if this tag will be part of some retrieved tag
>- <font color="DodgerBlue">string[]</font>``` attributesToRetrive``` - array of html attributes that will be retrieved with this tag

### AddCondition
```c#
tag.AddCondition(atribute, eaquals) 
```
adds condition to the tag it - will only take the tag from the html document if this condition is true    
>Parameters:
>- <font color="DodgerBlue">string</font>``` atribute``` - name of html tribute you are seting condition for,  
for example "class"
>- <font color="DodgerBlue">string</font>``` eaquals``` - value the tag must have to be retrieved

## Propreties

### IsInnerTag
```c#
public bool IsInnerTag { get; set; }
```
Bool indicating is this is inner tag

### TagToRetrive
```c#
public string TagToRetrive { get; private set; }
```
Name of html tag this class is refering to

### AttributesToRetrive
```c#
 public List<string> AttributesToRetrive { get; private set; }
```
List of attributes that will be retrieved with this tag


### XPath
```c#
public string XPath { get;}
```
XPath to this tag in html document 
