Imports System
Imports HTTR


Module Program
    Sub Main(args As String())
        Dim client = New HttrClient("https://github.com/jakubbinter/HTTR")
        'Create new HTTR Tag that will refer to html tag div
        Dim mtag = New HttrTag("p")
        'Add comdition to the tag so it will only take div tags where attribute class = "f4 my-3"
        mtag.AddCondition("class", "f4 my-3")
        'Create Httr Request, as parametr pass all tag you created
        Dim request = New HttrRequest(mtag)
        'Send request - your response wil be json string
        Dim resp = client.SendRequest(request)
        Console.WriteLine(resp)
    End Sub
End Module
