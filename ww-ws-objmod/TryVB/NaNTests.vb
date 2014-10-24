Public Class NaNTests
    Public Sub RunTests()
        Dim zero As Double

        Dim r1 As Double = (0 / zero) ' #IND - calculation does not make sense, like sqrt of negative #
        Dim r3 As Boolean = (Double.IsNaN(0 / zero))
        Dim r4 As Double = (1 / zero) ' #INF - larger than can be stored in a double
        Dim r7 As Boolean = (Double.IsInfinity(1 / zero))
        Dim r8 As Boolean = (Double.IsPositiveInfinity(1 / zero))
    End Sub
End Class
