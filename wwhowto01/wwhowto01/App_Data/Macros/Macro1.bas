'#Language "WWB.NET"

'#uses "Triangle.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Dim t As Triangle

    t = New Triangle(10, 10, 10, 0, 0, 0)
    t.Solve()
    Debug.Print(t.ToString())

    t = New Triangle(100, 10, 10, 0, 0, 0)
    t.Solve()
    Debug.Print(t.ToString())

    '(New Triangle(10, 10, 10, 0, 0, 0)).Solve()
End Sub
