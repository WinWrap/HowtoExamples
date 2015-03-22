'#Language "WWB.NET"

' To see ScriptLock in action:
' 1) start IDE#1
' 2) select IDE#2
' 3) start IDE#2
' Note that each IDE has exclusive access during the Using ScriptLock("port1") block.

Sub Main
    Debug.Print "Begin"
    Using ScriptLock("port1")
        Debug.Print "Lock port1"
        Debug.Print "Wait for 10 seconds"
        Wait 10
        Debug.Print "Unlock port1"
    End Using
    Debug.Print "End"
End Sub
