Public Class NaNTests
    Public Sub RunTests()
        Dim zero As Double
        Dim one As Double = 1

        Dim r1 As Double = (0 / zero) ' #IND - calculation does not make sense, like sqrt of negative #
        Dim r2 As Boolean = ((0 / zero) = Double.NaN)
        Dim r3 As Boolean = (Double.IsNaN(0 / zero))
        Dim r3b As Boolean = (Double.IsNaN(0 / 0))
        Dim r3c As Boolean = (Double.IsNaN(0 / 1))
        Dim r3d As Boolean = (Double.IsNaN(r1))
        Dim r4 As Double = (1 / zero) ' #INF - larger than can be stored in a double
        Dim r5 As Boolean = (1 / zero = Double.PositiveInfinity)
        Dim r6 As Boolean = (1 / zero = Double.NegativeInfinity)
        Dim r7 As Boolean = (Double.IsInfinity(1 / zero))
        Dim r8 As Boolean = (Double.IsPositiveInfinity(1 / zero))
        Dim r9 As Boolean = r1.NaN
        Dim r9b As Object = (1 / zero).NaN
        Dim r10 As Boolean = one.NaN
        Dim r11 As Boolean = r4.PositiveInfinity
        Dim r11b As Boolean = (1 / 0).PositiveInfinity
        Dim r11c As Boolean = (1 / 0).NegativeInfinity
        Dim r11d As Boolean = (0 / 0).PositiveInfinity
        Dim r11e As Boolean = (1 / 0).NaN
        Dim r12 As Boolean = r4.NegativeInfinity
    End Sub
End Class
