# Httr Tag
Class representing Html tag
## Methods
### Constructor
```c#
HttrTag tag=new HttrTag(tagToRetrive)
```
creates new HttrTag instance  
>Parameters:
>- <font color="DodgerBlue">string</font> ```tagToRetrive``` - name of html tag this class will represent

### AddCondition
```c#
tag.AddCondition(atribute, eaquals) 
```
adds condition to the tag it - will only take the tag from the html document if this condition is true    
>Parameters:
>- <font color="DodgerBlue">string</font> ```atribute``` - name of html tribute you are seting condition for,  
for example "class"
>- <font color="DodgerBlue">string</font> ```eaquals``` - value the tag must have to be retrieved

## Propreties

### TagToRetrive
```c#
public string TagToRetrive { get; private set; }
```
Name of html tag this class is refering to

### XPath
```c#
public string XPath { get;}
```
XPath to this tag in html document 
