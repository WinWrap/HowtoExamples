'#Language "WWB.NET"

Imports System
Imports System.Drawing
Imports System.Collections.Generic

Sub Main()
    ClientImage.DrawLine(20, 30, 100, 200)
    Dim t As New Triangle()
    t.MakeIsosceles()
    't.DoSort()
    'Dim tc As New TrianglePartComparer
    't.Parts = DoSort(t.Parts, tc)
    't.Parts = SortSides(t.Parts)
    t.SortSides()
    t.SortAngles()
    't.Parts = SortAngles(t.Parts)
    Dim s As String = t.Description()
    AppTrace(s)
    Dim d As Double = t.TheLawOfCosines(10, 10, 10)
    AppTrace(d.ToString())
    Dim a As Double = d * 180 / Math.PI()
    AppTrace(a.ToString())
End Sub

Imports System.Collections.Generic

' http://www.mathsisfun.com/algebra/trig-solving-triangles.html

Public Class TrianglePart
    Public Sub New(Optional aside As Double = Nothing, Optional aangle As Double = Nothing)
        Side = aside
        Angle = aangle
    End Sub
    Private Side_ As Double
    Private Angle_ As Double
    Property Side As Double
        Get
            Return Side_
        End Get
        Set(value As Double)
            Side_ = value
        End Set
    End Property
    Property Angle As Double
        Get
            Return Angle_
        End Get
        Set(value As Double)
            Angle_ = value
        End Set
    End Property
End Class

Public Class Triangle
    Private Parts_ As List(Of TrianglePart)
    Property Parts As List(Of TrianglePart)
        Get
            Return Parts_
        End Get
        Set(value As List(Of TrianglePart))
            Parts_ = value
        End Set
    End Property
    Public ReadOnly Property Sides() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Side <> Nothing Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Public ReadOnly Property Angles() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Angle <> Nothing Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Public Sub New()
        Parts = New List(Of TrianglePart)
    End Sub
    Public Sub Solve()
        If Sides = 3 And Angles = 3 Then Return
        If Sides = 3 Then SSS()
    End Sub
    Private Sub SSS()
        SortAngles()
    End Sub
    Public Sub SortSides()
        Parts = AppSortSides(Parts)
    End Sub
    Public Sub SortAngles()
        Parts = AppSortAngles(Parts)
    End Sub
    Public Function TheLawOfCosines(sidea As Double, sideb As Double, sidec As Double) As Double
        Dim anglec As Double
        anglec = Math.Acos((Math.Pow(sidec, 2) - Math.Pow(sidea, 2) - Math.Pow(sideb, 2)) / (2 * sidea * sideb))
        Return anglec
    End Function
    Private Sub TheLawOfSines()

    End Sub
    Private Sub AnglesOfaTriangle()

    End Sub
    Public Sub MakeIsosceles()
        Parts = New List(Of TrianglePart)
        Parts.Add(New TrianglePart(12))
        Parts.Add(New TrianglePart(aangle:=8))
        Parts.Add(New TrianglePart(10, 8))
    End Sub
    Public Function Description() As String
        Dim s As String = ""
        For Each part As TrianglePart In Parts
            Dim sPart As String = "(Side=" & PieceDescription(piece:=part.Side) & ", Angle=" & PieceDescription(part.Angle) & ")"
            s = IIf(s <> "", s & " ", s) & sPart
        Next
        Return s
    End Function
    Private Function PieceDescription(piece As Decimal) As String
        If piece = Nothing Then
            Return "Nothing"
        Else
            Return piece.ToString()
        End If
    End Function
End Class
