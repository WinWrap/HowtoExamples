'#Language "WWB.NET"

Imports System
Imports System.Drawing
'Imports System.Drawing.Imaging

Sub Main()
    'x()
    'Trace("zxcvz")
    ClientImage.DrawLine(20, 30, 100, 200)
    'ClientImage.DrawRectangle(300, 400, 100, 50)
    'ClientImage.FillRectangle(500, 400, 100, 50)
    'ClientImage.Gradient()
    'ClientImage.FillPolygon()
    'ClientImage.FillTriangle()
    Dim y As New Triangle()
    Dim s As String
    s = y.x
    Trace(s)
End Sub

Public Class Triangle
    Public Function x() As String
        Return "xxx"
    End Function
End Class
