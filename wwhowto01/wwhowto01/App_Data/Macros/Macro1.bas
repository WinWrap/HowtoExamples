'#Language "WWB.NET"

'#uses "Triangle.vb"
'#uses "Test.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    'Debug.Print(GetSideA())
    'SetSideA("3")
    'Debug.Print(SideB)
    'SideB = "4"
    'Dim b As Boolean = Test.RunAll()
    'Debug.Print(System.DateTime.Now.ToString())
    'Debug.Print(CType(New Triangle(10, 10, 0, 1.0471975511966, 0, 0), Triangle).Solve().ToString())
    Dim t As New Triangle(SideA, SideB, SideC, AngleA, AngleB, AngleC)
    t.Solve()
    SideA = t.Side(0)
    SideB = t.Side(1)
    SideC = t.Side(2)
    AngleA = t.Angle(0)
    AngleB = t.Angle(1)
    AngleC = t.Angle(2)
End Sub
