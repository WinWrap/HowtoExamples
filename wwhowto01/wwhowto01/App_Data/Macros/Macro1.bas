'#Language "WWB.NET"

'#uses "Triangle.vb"
'#uses "Test.vb"

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

    Debug.Print(CType(New Triangle(100, 10, 10, 0, 0, 0), Triangle).Solve().ToString())

    Dim b As Boolean = Test.RunAll()
    Debug.Print(b.ToString())
End Sub
