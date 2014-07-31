Public Class Form1
    Implements IDrawing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ScriptingLanguage.Initialize(Me)
        BasicIdeCtl1.AddScriptableObjectModel(GetType(ScriptingLanguage))
        BasicIdeCtl1.FileName = IO.Path.GetDirectoryName(Reflection.Assembly.GetEntryAssembly().Location) & "\..\..\Macro1.bas"
    End Sub

    Private Sub PictureBox1_Resize(sender As Object, e As EventArgs) Handles PictureBox1.Resize
        EraseLines()
    End Sub

#Region "Draw Ellipses"

    Private _iter As Stack(Of Integer) ' pop element for each ellipse
    Private _lim As Integer = 20 ' # of ellipses to draw

    ' initialize ellipses count and start timer
    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        _iter = New Stack(Of Integer)(Enumerable.Range(1, _lim).Reverse())
        Timer1.Enabled = True
    End Sub

    ' draw ellipse and pop stack
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim w As Integer = PictureBox1.Width
        Dim h As Integer = PictureBox1.Height
        Dim i As Integer = _iter.Pop
        DrawEllipse(0, 0, w * i / _lim - 1, h * i / _lim - 1)
        If _iter.Count = 0 Then Timer1.Enabled = False
    End Sub

    ' draw an ellipse into the drawing area
    Private Sub DrawEllipse(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
        Dim rect As Rectangle = New Rectangle(x1, y1, x2, y2)
        Using g As Graphics = Graphics.FromImage(_drawArea)
            g.DrawEllipse(Pens.Red, rect)
        End Using
        PictureBox1.Image = _drawArea
    End Sub

#End Region

#Region "IDrawing"

    Private _drawArea As Bitmap

    Public ReadOnly Property PictureHeight() As Integer Implements IDrawing.PictureHeight
        Get
            Return Me.PictureBox1.Height
        End Get
    End Property

    Public ReadOnly Property PictureWidth() As Integer Implements IDrawing.PictureWidth
        Get
            Return Me.PictureBox1.Width
        End Get
    End Property

    Public Sub DrawLine(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) Implements IDrawing.DrawLine
        Using g As Graphics = Graphics.FromImage(_drawArea)
            g.DrawLine(
                New Pen(Color.Blue, 2.0F),
                New Point(x1, y1),
                New Point(x2, y2))
        End Using
        PictureBox1.Image = _drawArea
    End Sub

    Public Sub EraseLines() Implements IDrawing.EraseLines
        _drawArea = New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height)
        PictureBox1.Image = _drawArea
    End Sub

#End Region

End Class
