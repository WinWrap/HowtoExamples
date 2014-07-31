Public Interface IDrawing
    ReadOnly Property PictureWidth As Integer
    ReadOnly Property PictureHeight As Integer
    Sub DrawLine(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
    Sub EraseLines()
End Interface
