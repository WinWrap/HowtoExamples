﻿'#Language "WWB.NET"

Imports System
Imports System.Drawing
Imports System.Collections.Generic

Sub Main()
    AppTrace(System.DateTime.Now.ToString())
    ClientImage.DrawLine(20, 30, 100, 200)
    Dim cr As New CosineRule()
    Dim s As String = cr.xToString()
    Dim d As Double = cr.Solve()
    AppTrace(d.ToString())
End Sub

Public Class Triangle
    ' http://www.mathsisfun.com/algebra/trig-solving-triangles.html
    ' handle illegal combination of triangle parts xxx
    Public Parts As List(Of TrianglePart)
    Public Sub New()
        Parts = New List(Of TrianglePart)
    End Sub
    Public Function Solve() As Boolean
        If Solved Then Return True
        If Sides = 3 Then Return SSS()
        If Angles = 2 Then Return AA()
        If Sides = 2 And Angles = 1 Then Return SAS
    End Function
    Private Function SAS() As Boolean
        ' Angle between
        SortSides()
        'Parts(0).Side = TheLawOfConsinesSAS(Parts(0).Angle, Parts(1).Side, Parts(2).Side)
        Return Solve()
    End Function
    Private Function AA() As Boolean
        SortAngles()
        Parts(0).Angle = AnglesOfaTriangle(Parts(1).Angle, Parts(2).Angle)
        Return Solve()
    End Function
    Public Function TheLawOfCosinesSAS(anglea As Double, sideb As Double, sidec As Double) As Double
        Dim a As Double = sideb
        Dim b As Double = sidec
        Dim anglec As Double = anglea
        Return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - 2 * a * b * Math.Cos(anglec))
    End Function
    Public Function TheLawOfCosinesSSS(sidea As Double, sideb As Double, sidec As Double) As Double
        Dim anglec As Double
        anglec = Math.Acos((Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - Math.Pow(sidec, 2)) / (2 * sidea * sideb))
        Return anglec
    End Function
    Private Function AnglesOfaTriangle(angleb As Double, anglec As Double) As Double
        Return Math.PI - angleb - anglec
    End Function
    Public Function xTheLawOfCosines(sidea As Double, sideb As Double, sidec As Double) As Double
        Dim anglec As Double
        anglec = Math.Acos((Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - Math.Pow(sidec, 2)) / (2 * sidea * sideb))
        Return anglec
    End Function
    Private Function SSS() As Boolean
        SortAngles()
        Parts(0).Angle = TheLawOfCosinesSSS(Parts(0).Side, Parts(1).Side, Parts(2).Side)
        Return Solve()
    End Function
    Public Sub SortSides()
        Parts = AppSortSides(Parts)
    End Sub
    Public Sub SortAngles()
        Parts = AppSortAngles(Parts)
    End Sub
    Private ReadOnly Property Solved() As Boolean
        Get
            Return (Sides >= 3) And (Angles >= 3)
        End Get
    End Property
    Private ReadOnly Property Sides() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Side <> Nothing Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Private ReadOnly Property Angles() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Angle <> Nothing Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Public Function ToString() As String
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

Public Class TrianglePart
    Public Side As Double
    Public Angle As Double
    Public Sub New(Optional aside As Double = Nothing, Optional aangle As Double = Nothing)
        Side = aside
        Angle = aangle
    End Sub
End Class

Public Class xCosineRule ' Static xxx
    Public SideA As Double
    Public SideB As Double
    Public SideC As Double
    Public AngleC As Double
    Public Sub New()
        SideA = Nothing
        SideB = Nothing
        SideC = Nothing
        AngleC = Nothing
    End Sub
    Public Function SolveSideC() As Double
        Return Math.Sqrt(Math.Pow(SideA, 2) + Math.Pow(SideB, 2) - 2 * SideA * SideB * Math.Cos(AngleC))
    End Function
    Public Function SolveAngleC() As Double
        'anglec = Math.Acos((Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - Math.Pow(sidec, 2)) / (2 * sidea * sideb))
        Return Math.Acos((Math.Pow(SideA, 2) + Math.Pow(SideB, 2) - Math.Pow(SideC, 2)) / (2 * SideA * SideB))
    End Function
End Class

Public Enum CosineRuleEnum
    SAS
    SSA
    SSS
End Enum
Public Class CosineRule
    Private Rule As CosineRuleEnum
    Private Sides As List(Of Double)
    Public Sub New(Optional aRule As CosineRuleEnum = CosineRuleEnum.SSS, Optional aSides As List(Of Double) = Nothing)
        Rule = aRule
        Sides = If(aSides IsNot Nothing, aSides, New List(Of Double)(New Double() {10, 10, 10}))
    End Sub
    Public Function Solve() As Double
        Dim result As Double
        Select Case Rule
            Case CosineRuleEnum.SSS
                result = SolveSSS()
            Case Else
                ' throw error
                ' if angle > pi / 180
                result = 0
        End Select
        Return result
    End Function
    Public Function SolveSSS() As Double
        Return Math.Acos((Math.Pow(Sides(0), 2) + Math.Pow(Sides(1), 2) - Math.Pow(Sides(2), 2)) / (2 * Sides(0) * Sides(1)))
    End Function
    Public Function xToString() As String
        Dim sSides As String = ""
        For Each side As Double In Sides
            sSides = sSides & IIf(String.IsNullOrEmpty(sSides), "", ", ") & side.ToString()
        Next
        Dim result As String = String.Format("{0}: {1}", Rule.ToString(), sSides)
        Return result
    End Function
End Class

