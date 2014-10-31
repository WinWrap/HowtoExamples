Public Class TrianglePartSideComparer
    Implements IComparer(Of TrianglePart)
    Public Function Compare(x As TrianglePart, y As TrianglePart) As Integer Implements IComparer(Of TrianglePart).Compare
        Return x.Side.CompareTo(y.Side)
    End Function
End Class

Public Class TrianglePartAngleComparer
    Implements IComparer(Of TrianglePart)
    Public Function Compare(x As TrianglePart, y As TrianglePart) As Integer Implements IComparer(Of TrianglePart).Compare
        Return x.Angle.CompareTo(y.Angle)
    End Function
End Class

Public Class Triangle
    ' http://www.mathsisfun.com/algebra/trig-solving-triangles.html
    Public Parts As List(Of TrianglePart)
    Public Sub New()
        'AppTrace(System.DateTime.Now.ToString())
        Parts = New List(Of TrianglePart)
    End Sub
    Public Function Solve() As Boolean
        If Solved Then Return True
        If Sides = 3 Then SSS()
        If Angles = 2 Then AA()
        If IsAAS() Then AAS()
        If IsSAS() Then SAS()
        If IsSSA() Then SSA()
        Return Solve()
    End Function
    Private Function IsAAS() As Boolean
        If Angles < 2 Then Return False
        If Sides < 1 Then Return False ' invalid
        SortAngles()
        SortSides()
        Return (Parts(1).Side = 0)
    End Function
    Private Sub AAS()
        SortSides()
        SortAngles() ' 2-way sort?
        Parts(1).Side = Parts(2).Side * Math.Sin(Parts(1).Angle) / Math.Sin(Parts(2).Angle)
    End Sub
    Private Function IsSAS() As Boolean
        If Sides <> 2 Then Return False
        SortSides()
        Return (Parts(0).Angle <> 0)
    End Function
    Private Function IsSSA() As Boolean
        If Sides <> 2 Then Return False
        SortAngles()
        Return (Parts(2).Side <> 0)
    End Function
    Private Sub SSA()
        SortSides()
        SortAngles()
        Parts(1).Angle = Math.ASin(Math.Sin(Parts(2).Angle) * Parts(1).Side / Parts(2).Side)
    End Sub
    Private Sub SAS()
        SortSides()
        Dim cr As New CosineRule(CosineRuleEnum.SAS, New List(Of Double)(New Double() {Parts(1).Side, Parts(0).Angle, Parts(2).Side}))
        Parts(0).Side = cr.Solve()
    End Sub
    Private Sub AA()
        SortAngles()
        Parts(0).Angle = Math.PI - Parts(1).Angle - Parts(2).Angle
    End Sub
    Private Sub SSS()
        SortAngles()
        Dim cr As New CosineRule(CosineRuleEnum.SSS, New List(Of Double)(New Double() {Parts(0).Side, Parts(1).Side, Parts(2).Side}))
        Parts(0).Angle = cr.Solve()
    End Sub
    Public Sub SortSides()
        Dim tc As New TrianglePartSideComparer
        Parts.Sort(tc)
    End Sub
    Public Sub SortAngles()
        Dim tc As New TrianglePartAngleComparer
        Parts.Sort(tc)
    End Sub
    Public ReadOnly Property Solved() As Boolean
        Get
            Return (Sides >= 3) And (Angles >= 3)
        End Get
    End Property
    Private ReadOnly Property Sides() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Side <> 0 Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Private ReadOnly Property Angles() As Integer
        Get
            Dim cnt As Integer = 0
            For Each part As TrianglePart In Parts
                If part.Angle <> 0 Then cnt = cnt + 1
            Next
            Return cnt
        End Get
    End Property
    Public Function MakeString() As String
        Dim s As String = ""
        For Each part As TrianglePart In Parts
            Dim sPart As String = "(Side=" & PieceDescription(piece:=part.Side) & ", Angle=" & PieceDescription(part.Angle) & ")"
            s = IIf(s <> "", s & " ", s) & sPart
        Next
        Return s
    End Function
    Private Function PieceDescription(piece As Double) As String
        If piece = 0 Then
            Return "0(Empty)"
        Else
            Return piece.ToString()
        End If
    End Function
End Class

Public Class TrianglePart
    Public Side As Double
    Public Angle As Double
    Public Sub New(Optional aside As Double = 0, Optional aangle As Double = 0)
        Side = aside
        Angle = aangle
    End Sub
End Class

Public Enum CosineRuleEnum
    SAS
    SSS
End Enum
Public Class CosineRule
    Private Rule As CosineRuleEnum
    Private Datums As List(Of Double)
    Public Sub New(Optional aRule As CosineRuleEnum = CosineRuleEnum.SSS, Optional aDatums As List(Of Double) = Nothing)
        Rule = aRule
        Datums = If(aDatums IsNot Nothing, aDatums, New List(Of Double)(New Double() {10, 10, 10}))
        'Datums = If(aDatums IsNot Nothing, aDatums, New List(Of Double)(New Double() {10, 1.0471975511966, 10}))
    End Sub
    Public Function Solve() As Double
        Dim result As Double
        Select Case Rule
            Case CosineRuleEnum.SSS
                result = SolveSSS()
            Case CosineRuleEnum.SAS
                result = SolveSAS()
            Case Else
                ' throw error
                ' if angle > pi / 180
                result = 0
        End Select
        Return result
    End Function
    Public Function SolveSAS() As Double ' side
        Dim sidea As Double = Datums(0)
        Dim sideb As Double = Datums(2)
        Dim anglec As Double = Datums(1)
        Return Math.Sqrt(Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - 2 * sidea * sideb * Math.Cos(anglec))
    End Function
    Public Function SolveSSS() As Double ' angle
        Dim sidea As Double = Datums(0)
        Dim sideb As Double = Datums(1)
        Dim sidec As Double = Datums(2)
        Return Math.Acos((Math.Pow(sidea, 2) + Math.Pow(sideb, 2) - Math.Pow(sidec, 2)) / (2 * sidea * sideb))
    End Function
    Public Function MakeString() As String
        Dim sSides As String = ""
        For Each side As Double In Datums
            sSides = sSides & IIf(String.IsNullOrEmpty(sSides), "", ", ") & side.ToString()
        Next
        Dim result As String = String.Format("{0}: {1}", Rule.ToString(), sSides)
        Return result
    End Function
End Class
