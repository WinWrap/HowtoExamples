'#Language "WWB.NET"

'#uses "Triangle.vb"
'#uses "Test.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Dim b As Boolean = Test.RunAll()
    'Debug.Print(System.DateTime.Now.ToString())
    'Debug.Print(CType(New Triangle(10, 10, 0, 1.0471975511966, 0, 0), Triangle).Solve().ToString())
End Sub
