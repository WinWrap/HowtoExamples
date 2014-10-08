
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
        public static void Trace(string msg)
        {
            AppModel.Trace(msg);
        }
    }
}
