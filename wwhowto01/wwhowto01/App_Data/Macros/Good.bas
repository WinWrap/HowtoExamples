'#Language "WWB.NET"

'#uses "local/Triangle.vb"

Imports System

' Subscribe events for object mangaged by host (TheIncident)
' http://www.winwrap.com/web/basic/language/?p=doc_withevents__def.htm
'Dim WithEvents anincident1 As Incident = TheIncident
Dim WithEvents anincident1 As ClientImage = TriangleImage

Private Sub anincident1_Started() Handles anincident1.Started
    'Debug.Print("asdf")
    'xxx()
    'anincident1.EraseLines()
    Dim t As New Triangle(SideA, SideB, SideC, AngleA, AngleB, AngleC)
    t.Solve()
    SideA = t.Side(0)
    SideB = t.Side(1)
    SideC = t.Side(2)
    AngleA = t.Angle(0)
    AngleB = t.Angle(1)
    AngleC = t.Angle(2)
    Dim scale As Integer = 100
    Dim w As Integer = TriangleImage.Width
    Dim h As Integer = TriangleImage.Height
    'TriangleImage.DrawLine(0, 0, w, h)
    Dim sa As Integer = SideA * scale
    Dim sb As Integer = SideB * scale
    Dim sc As Integer = SideC * scale
    TriangleImage.DrawLine(0, 0, sc, 0)
    Dim d As Double = AngleA
    'Debug.Print(d.ToString())
    Dim x2 As Integer = sb * Math.Cos(AngleA)
    Dim y2 As Integer = sb * Math.Sin(AngleA)
    TriangleImage.DrawLine(0, 0, x2, y2)
    Dim x1 As Integer = x2 + sa * Math.Cos(AngleB)
    Dim y1 As Integer = y2 - sa * Math.Sin(AngleB)
    TriangleImage.DrawLine(x1, y1, x2, y2)
End Sub
