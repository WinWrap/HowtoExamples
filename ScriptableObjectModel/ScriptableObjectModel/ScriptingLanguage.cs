
namespace ScriptableObjectModel
{
    [Scriptable] public static class ScriptingLanguage
    {
        internal static IDrawing Drawing { get; private set; }

        public static void SetDrawing(IDrawing drawing)
        {
            Drawing = drawing;
        }

        [Scriptable] public static int PictureWidth { get { return Drawing.PictureWidth; } }
        [Scriptable] public static int PictureHeight { get { return Drawing.PictureHeight; } }

        [Scriptable] public static void DrawLine(int x1, int y1, int x2, int y2)
        {
            Drawing.DrawLine(x1, y1, x2, y2);
        }

        [Scriptable] public static void EraseLines()
        {
            Drawing.EraseLines();
        }
    }
}
