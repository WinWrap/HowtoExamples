'#Language "WWB.NET"

'#uses "Triangle.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Dim t As New Triangle(10, 10, 10, 0, 0, 0)
    Debug.Print(t.ToString())
    'Debug.Print(TypeName(t))
    t.Solve()
    Debug.Print(t.ToString())

    '(New Triangle(10, 10, 10, 0, 0, 0)).Solve
    'Debug.Print("Hello")
End Sub
