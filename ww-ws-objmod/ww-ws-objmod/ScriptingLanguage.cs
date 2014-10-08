
namespace ww_ws_objmod
{
    [Scriptable]
    public static class ScriptingLanguage
    {
        internal static IAppModel AppModel { get; private set; }

        // connect the scripting language to the IDrawing instance
        public static void SetAppModel(IAppModel appmodel)
        {
            AppModel = appmodel;
        }

        [Scriptable]
        public static ClientImage ClientImage { get { return AppModel.ClientImage; } }

        [Scriptable]
        public static int PictureWidth { get { return AppModel.PictureWidth; } }

        [Scriptable]
        public static void EraseLines(string s)
        {
            AppModel.EraseLines(s);
        }

        /*[Scriptable]
        public static void DrawLine(string s)
        {
            AppModel.DrawLine(s);
        }*/

        /*[Scriptable]
        public static void ErrorAppend(string serror)
        {
            AppModel.ErrorAppend(serror);
        }*/
    }
}
