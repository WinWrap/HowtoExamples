
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
        public static AClass TheAClass { get { return AppModel.TheAClass; } }

        [Scriptable]
        public static int PictureWidth { get { return AppModel.PictureWidth; } }

        [Scriptable]
        public static void EraseLines()
        {
            AppModel.EraseLines();
        }

        /*[Scriptable]
        public static void ErrorAppend(string serror)
        {
            AppModel.ErrorAppend(serror);
        }*/
    }
}
