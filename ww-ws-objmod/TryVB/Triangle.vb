Imports System.Collections.Generic

' http://www.mathsisfun.com/algebra/trig-solving-triangles.html


Public Enum CosineRuleEnum
    SAS
    SSS
End Enum
Public Class CosineRule
    Private Rule As CosineRuleEnum
    Private Datums As List(Of Double)
    Public Sub New(Optional aRule As CosineRuleEnum = CosineRuleEnum.SSS, Optional aDatums As List(Of Double) = Nothing)
        Rule = aRule
        Datums = If(aDatums IsNot Nothing, aDatums, New List(Of Double)(New Double() {10, 10, 10}))
        'Datums = If(aDatums IsNot Nothing, aDatums, New List(Of Double)(New Double() {10, 1.0471975511966, 10}))
    End Sub
    Public Function Solve() As Double
        Dim result As Double
        Select Case Rule
            Case CosineRuleEnum.SSS
                result = SolveSSS()
            Case CosineRuleEnum.SAS
                result = SolveSAS()
            Case Else
                ' throw error
                ' if angle > pi / 180
                result = 0
        End Select
        'If Double.IsNaN(result.NaN) Then
        'Return -1
        'End If
        Return result
    End Function
    Public Function SolveSAS() As Double
        Dim sidea As Double = Datums(0)
        Dim sideb As Double = Datums(2)
        Dim anglec As Double = Datums(1)
        Return Math.Sqrt(Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - 2 * sidea * sideb * Math.Cos(anglec))
    End Function
    Public Function SolveSSS() As Double
        Dim sidea As Double = Datums(0)
        Dim sideb As Double = Datums(1)
        Dim sidec As Double = Datums(2)
        Return Math.Acos((Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - Math.Pow(sidec, 2)) / (2 * sidea * sideb))
    End Function
    Public Function xToString() As String
        Dim sSides As String = ""
        For Each side As Double In Datums
            sSides = sSides & IIf(String.IsNullOrEmpty(sSides), "", ", ") & side.ToString()
        Next
        Dim result As String = String.Format("{0}: {1}", Rule.ToString(), sSides)
        Return result
    End Function
End Class