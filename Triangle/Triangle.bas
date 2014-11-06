Attribute VB_Name = "Triangle"
'#Language "WWB.NET"

Imports System
Imports System.Collections.Generic

Public Class Triangle
    ' http://www.mathsisfun.com/algebra/trig-solving-triangles.html
    Public Parts As New List(Of TrianglePart)

    Public Function Solve(Optional depth As Integer = 0) As Boolean
        If Solved Then Return True
        If depth > 5 Then Throw New Exception("Unsolvable")
        If Sides = 3 Then SSS()
        If Angles = 2 Then AA()
        If IsAAS() Then AAS()
        If IsSAS() Then SAS()
        If IsSSA() Then SSA()
        Return Solve(depth + 1)
    End Function

    Private Function IsAAS() As Boolean
        If Angles < 2 Then Return False
        If Sides < 1 Then Return False ' invalid
        SortSides()
        Return Parts(1).Side = 0
    End Function

    Private Sub AAS()
        SortAngles()
        'Debug.Print "AAS: " & ToString
        Parts(1).Side = Parts(2).Side * Math.Sin(Parts(1).Angle) / Math.Sin(Parts(2).Angle)
    End Sub

    Private Function IsSAS() As Boolean
        If Sides <> 2 Then Return False
        SortSides()
        Return Parts(0).Angle <> 0
    End Function

    Private Function IsSSA() As Boolean
        If Sides <> 2 Then Return False
        SortAngles()
        Return Parts(2).Side <> 0
    End Function

    Private Sub SSA()
        SortAngles()
        Parts(1).Angle = Math.ASin(Math.Sin(Parts(2).Angle) * Parts(1).Side / Parts(2).Side)
    End Sub

    Private Sub SAS()
        SortSides()
        Parts(0).Side = Math.Sqrt(Parts(1).Side^2 + Parts(1).Side^2 - 2 * Parts(1).Side * Parts(1).Side * Math.Cos(Parts(0).Angle))
    End Sub

    Private Sub AA()
        SortAngles()
        Parts(0).Angle = Math.PI - Parts(1).Angle - Parts(2).Angle
    End Sub

    Private Sub SSS()
        SortAngles()
        Parts(0).Angle = Math.Acos((Parts(0).Side^2 + Parts(1).Side^2 - Parts(2).Side^2) / (2 * Parts(0).Side * Parts(1).Side))
    End Sub

    Public Sub SortSides()
        Dim tc As New TrianglePartSideComparer
        Parts.Sort(tc)
    End Sub

    Public Sub SortAngles()
        Dim tc As New TrianglePartAngleComparer
        Parts.Sort(tc)
    End Sub

    Public ReadOnly Property Solved() As Boolean
        Get
            Return (Sides >= 3) And (Angles >= 3)
        End Get
    End Property

    Private ReadOnly Property Sides() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Side <> 0 Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property

    Private ReadOnly Property Angles() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Angle <> 0 Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property

    Private Function PieceDescription(piece As Double) As String
        If piece = 0 Then
            Return "0(Empty)"
        Else
            Return piece.ToString()
        End If
    End Function

    Public Overrides Function ToString() As String
        Dim s As String = ""
        For Each part As TrianglePart In Parts
            Dim sPart As String = "(Side=" & PieceDescription(piece:=part.Side) & ", Angle=" & PieceDescription(part.Angle) & ")"
            s = If(s <> "", s & " ", s) & sPart
        Next
        Return s
    End Function
End Class

Public Class TrianglePart
    Public Side As Double
    Public Angle As Double

    Public Sub New(Optional aside As Double = 0, Optional aangle As Double = 0)
        Side = aside
        Angle = aangle
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Side={0}, Angle={1}", Side, Angle)
    End Function
End Class

Public Class TrianglePartSideComparer
    Implements IComparer(Of TrianglePart)
    Public Function Compare(x As TrianglePart, y As TrianglePart) As Integer Implements IComparer(Of TrianglePart).Compare
        Return If(x.Side = y.Side, x.Angle.CompareTo(y.Angle), x.Side.CompareTo(y.Side))
    End Function
End Class

Public Class TrianglePartAngleComparer
    Implements IComparer(Of TrianglePart)
    Public Function Compare(x As TrianglePart, y As TrianglePart) As Integer Implements IComparer(Of TrianglePart).Compare
        Return If(x.Angle = y.Angle, x.Side.CompareTo(y.Side), x.Angle.CompareTo(y.Angle))
    End Function
End Class
