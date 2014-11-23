'#Language "WWB.NET"

Imports System

Sub Main()
    Dim scale As Integer = 100
    Dim w As Integer = TriangleImage.Width
    Dim h As Integer = TriangleImage.Height
    'TriangleImage.DrawLine(0, 0, w, h)
    Dim sa As Integer = SideA * scale
    Dim sb As Integer = SideB * scale
    Dim sc As Integer = SideC * scale
    TriangleImage.DrawLine(0, 0, sc, 0)
    Dim x2 As Integer = sb * Math.Cos(AngleA)
    Dim y2 As Integer = sb * Math.Sin(AngleA)
    TriangleImage.DrawLine(0, 0, x2, y2)
    Dim x1 As Integer = x2 + sa * Math.Cos(AngleB)
    Dim y1 As Integer = y2 - sa * Math.Sin(AngleB)
    TriangleImage.DrawLine(x1, y1, x2, y2)
End Sub
