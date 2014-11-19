﻿'#Language "WWB.NET"

'#uses "Triangle.vb"
'#uses "Test.vb"

Imports System
Imports System.Collections.Generic

Sub Main()
    Dim t As New Triangle(SideA, SideB, SideC, AngleA, AngleB, AngleC)
    t.Solve()
    SideA = t.Side(0)
    SideB = t.Side(1)
    SideB = t.Side(0)
    SideC = t.Side(2)
    AngleA = t.Angle(0)
    AngleB = t.Angle(1)
    AngleC = t.Angle(2)
End Sub
