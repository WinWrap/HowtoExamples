Imports System.Collections.Generic

' http://www.mathsisfun.com/algebra/trig-solving-triangles.html


Public Class CosineRule
    Public Enum xKnown
        SAS
        SSA
        SSS
    End Enum
    Public Known As xKnown
    Public Pieces As List(Of Double)
    Public Sub New(aKnown As xKnown, aPieces As List(Of Double))
        Known = aKnown
        Pieces = aPieces
    End Sub
    Public Overrides Function ToString() As String
        Return "asdf"
    End Function
End Class