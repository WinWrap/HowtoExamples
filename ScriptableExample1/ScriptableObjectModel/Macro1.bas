'#Language "WWB.NET"

Imports System

Sub Main()
    EraseLines()
    Dim w As Integer = PictureWidth
    Dim h As Integer = PictureHeight
    Dim lim As Integer = 64
    For i As Integer = 0 To lim-1
        Dim a = Math.PI * 2 * i / lim
        Dim x = w / 2 + Cos(a) * w / 2
        Dim y = h / 2 + Sin(a) * h / 2
        DrawLine(w / 2, h / 2, x, y)
        Wait 0.1
    Next
End Sub
