'#Language "WWB.NET"

'#uses "Triangle.vb"

Public Module Test
    Public Function RunAll() As Boolean
        'Dim d As Double = Math.Acos(-1.01)
        'Dim t As New Triangle(10, 10, 10, 0, 0, 0)
        't.Solve()
        'Debug.Print(t.ToString())
        'Debug.Print(CType(New Triangle(10, 10, 10, 0, 0, 0), Triangle).Solve().ToString())
        'Debug.Print(CType(New Triangle(10, 10, 0, 1.0471975511966, 0, 0), Triangle).Solve().ToString())
        Debug.Print(CType(New Triangle(10, 10, 0, 60, 0, 0), Triangle).Solve().ToString())
        'Debug.Print(CType(New Triangle(100, 10, 10, 0, 0, 0), Triangle).Solve().ToString())
        Return True
    End Function
End Module