<Scriptable> Public Module ScriptingLanguage
    Private _IDrawing As IDrawing

    Public Sub Initialize(aIDrawing As IDrawing)
        _IDrawing = aIDrawing
    End Sub

    <Scriptable>
    Public ReadOnly Property PictureWidth() As Integer
        Get
            Return _IDrawing.PictureWidth
        End Get
    End Property

    <Scriptable>
    Public ReadOnly Property PictureHeight() As Integer
        Get
            Return _IDrawing.PictureHeight
        End Get
    End Property

    <Scriptable>
    Public Sub DrawLine(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
        _IDrawing.DrawLine(x1, y1, x2, y2)
    End Sub

    <Scriptable>
    Public Sub EraseLines()
        _IDrawing.EraseLines()
    End Sub

End Module
