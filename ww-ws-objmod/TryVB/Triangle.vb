Imports System.Collections.Generic

' http://www.mathsisfun.com/algebra/trig-solving-triangles.html


Public Enum CosineRuleEnum
    SAS
    SSA
    SSS
End Enum
Public Class CosineRule
    Private Rule As CosineRuleEnum
    Private Sides As List(Of Double)
    Public Sub New(Optional aRule As CosineRuleEnum = CosineRuleEnum.SAS, Optional aSides As List(Of Double) = Nothing)
        Rule = aRule
        Sides = If(aSides IsNot Nothing, aSides, New List(Of Double)(New Double() {10, 1.0471975511966, 10}))
    End Sub
    Public Function Solve() As Double
        Dim result As Double
        Select Case Rule
            Case CosineRuleEnum.SSS
                result = SolveSSS()
            Case CosineRuleEnum.SAS
                result = SolveSAS()
            Case CosineRuleEnum.SSA
                result = SolveSSA()
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
        Return 1
    End Function
    Public Function SolveSSA() As Double
        Return 1
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