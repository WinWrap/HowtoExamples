'#Language "WWB.NET"

Imports System
Imports System.Collections.Generic

Public Class Triangle
    ' http://www.mathsisfun.com/algebra/trig-solving-triangles.html
    Private Corners As New List(Of TriangleCorner)

    Public Sub New(Sa As Double, Sb As Double, Sc As Double,
        AA As Double, AB As Double, AC As Double)
        Corners.add(New TriangleCorner("A", Sa, AA))
        Corners.add(New TriangleCorner("B", Sb, AB))
        Corners.Add(New TriangleCorner("C", Sc, AC))
        Debug.Print("New: " & ToString() & " at " & System.DateTime.Now.ToString())
    End Sub

    Public Function Angle(index As Integer) As Double
        Return Corners(index).Angle
    End Function

    Public Function Side(index As Integer) As Double
        Return Corners(index).Side
    End Function

    Public Sub Solve()
        Dim n As Integer
        Do
            n = Sides + Angles
            If Angles = 2 Then TryAA()
            If Sides = 3 Then TrySSS()
            If Sides = 2 AndAlso Angles >= 1 Then TrySAS()
            If Sides = 2 AndAlso Angles >= 1 Then TrySSA()
            If Sides = 1 AndAlso Angles >= 2 Then TryAAS()
        Loop While Sides + Angles > n ' continue while making progress
        SortNames()
    End Sub

    Public ReadOnly Property Solved As Boolean
        Get
            Return Sides = 3 AndAlso Angles = 3
        End Get
    End Property


    Private Sub TrySSA()
        SortAngles()
        If Side(2) <> 0 AndAlso Angle(1) = 0 Then
            ' Law of Sines: a/sin(A) = b/sin(B) = c/sin(C)
            ' solve for B: sin(B)/b = sin(C)/c
            ' solve for B: B = asin(sin(C)*b/c)
            ' side b is Side(1)
            ' side c is Side(2)
            ' angle C is Angle(2)
            Dim asin = Math.Sin(Angle(2)) * Side(1) / Side(2)
            ' cope with asin slightly out of range
            If asin < -1 Then asin = -1
            If asin > 1 Then asin = 1
            Corners(1).Angle = Math.ASin(asin)
        End If
    End Sub

    Private Sub TrySAS()
        SortSides()
        If Angle(0) <> 0 AndAlso Side(0) = 0 Then
            ' Law of Cosines: a^2 = b^2 + c^2 - 2*b*c*cos(A)
            ' solve for a: a = sqrt(b^2 + c^2 - 2*b*c*cos(A))
            ' angle A is Angle(0)
            ' side b is Side(1)
            ' side c Side(2)
            ' side a is Side(0)
            Corners(0).Side = Math.Sqrt(Side(1) ^ 2 + Side(2) ^ 2 - 2 * Side(1) * Side(2) * Math.Cos(Angle(0)))
        End If
    End Sub

    Private Sub TryAA()
        SortAngles()
        If Angle(0) = 0 Then
            ' Law of Angles: A + B + C = 180
            ' solve for A: A = 180 - B - C
            ' angle A is Angle(0)
            ' angle B is Angle(1)
            ' angle C is Angle(2)
            Corners(0).Angle = Math.PI - Angle(1) - Angle(2)
        End If
    End Sub

    Private Sub TryAAS()
        SortSides()
        If Side(1) = 0 Then
            SortAngles()
            ' Law of Sines: a/sin(A) = b/sin(B) = c/sin(C)
            ' solve for b: b = c*sin(B)/sin(C)
            ' side b is Side(1)
            ' angle B is Angle(1)
            ' side c is Side(2)
            ' angle C is Angle(2)
            Corners(1).Side = Side(2) * Math.Sin(Angle(1)) / Math.Sin(Angle(2))
        End If
    End Sub

    Private Sub TrySSS()
        SortAngles()
        If Angle(0) = 0 Then
            ' Law of Cosines: a^2 = b^2 + c^2 - 2*b*c*cos(A)
            ' solve for A: A = acos((b^2 + c^2 - a^2)/(2*b*c))
            ' side a is Side(0)
            ' side b is Side(1)
            ' side c is Side(2)
            ' angle A is Angle(0)
            Corners(0).Angle = Math.Acos((Side(1) ^ 2 + Side(2) ^ 2 - Side(0) ^ 2) / (2 * Side(1) * Side(2)))
        End If
    End Sub

    Private Sub SortNames()
        Dim tc As New TriangleCornerNameComparer
        Corners.Sort(tc)
    End Sub

    Private Sub SortSides()
        Dim tc As New TriangleCornerSideComparer
        Corners.Sort(tc)
    End Sub

    Private Sub SortAngles()
        Dim tc As New TriangleCornerAngleComparer
        Corners.Sort(tc)
    End Sub

    Private ReadOnly Property Sides() As Integer
        Get
            Dim cnt As Integer = 0
            For Each Corner As TriangleCorner In Corners
                If Corner.Side <> 0 Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property

    Private ReadOnly Property Angles() As Integer
        Get
            Dim cnt As Integer = 0
            For Each Corner As TriangleCorner In Corners
                If Corner.Angle <> 0 Then cnt = cnt + 1
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
        For Each Corner As TriangleCorner In Corners
            Dim sCorner As String = "(Side=" & PieceDescription(Corner.Side) & ", Angle=" & PieceDescription(Corner.Angle * 180 / Math.Pi) & ")"
            s = If(s <> "", s & " ", s) & sCorner
        Next
        Return s
    End Function
End Class

Public Class TriangleCorner
    Public Name As String
    Public Side As Double
    Public Angle As Double

    Public Sub New(aname As String, aside As Double, aangle As Double)
        Name = aname
        Side = aside
        Angle = aangle
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("Side={0}, Angle={1}", Side, Angle)
    End Function
End Class

Public Class TriangleCornerNameComparer
    Implements IComparer(Of TriangleCorner)
    Public Function Compare(x As TriangleCorner, y As TriangleCorner) As Integer Implements IComparer(Of TriangleCorner).Compare
        Return x.Name.CompareTo(y.Name)
    End Function
End Class

Public Class TriangleCornerSideComparer
    Implements IComparer(Of TriangleCorner)
    Public Function Compare(x As TriangleCorner, y As TriangleCorner) As Integer Implements IComparer(Of TriangleCorner).Compare
        Return If(x.Side = y.Side, x.Angle.CompareTo(y.Angle), x.Side.CompareTo(y.Side))
    End Function
End Class

Public Class TriangleCornerAngleComparer
    Implements IComparer(Of TriangleCorner)
    Public Function Compare(x As TriangleCorner, y As TriangleCorner) As Integer Implements IComparer(Of TriangleCorner).Compare
        Return If(x.Angle = y.Angle, x.Side.CompareTo(y.Side), x.Angle.CompareTo(y.Angle))
    End Function
End Class
