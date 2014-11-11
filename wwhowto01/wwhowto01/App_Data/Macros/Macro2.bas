'#Language "WWB.NET"

'#uses "Triangle.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Dim t As New Triangle(10, 10, 10, 0, 0, 0)
    t.Solve()
    Debug.Print(t.ToString())
End Sub
