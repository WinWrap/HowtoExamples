'#Language "WWB.NET"

'#uses "Triangle.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Debug.Print("Hello")
    Dim t As New Triangle(10, 10, 10, 0, 0, 0)
    t.Solve()
    'AppTrace("t.Test()")
    AppTrace(System.DateTime.Now.ToString())
    AppTrace(t.ToString())
End Sub
