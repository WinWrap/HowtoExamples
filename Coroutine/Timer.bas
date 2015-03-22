'#Language "WWB.NET"

Sub Main
    Dim j As Integer
    Dim t0 As Date = Now
    For i As Integer = 1 To 100000000
        j = j + 1
    Next
    Dim t1 As Date = Now
    Debug.Print t1-t0
End Sub
