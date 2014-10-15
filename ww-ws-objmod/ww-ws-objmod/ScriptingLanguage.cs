using System.Collections.Generic;

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
        public static void AppTrace(string msg)
        {
            AppModel.AppTrace(msg);
        }

        [Scriptable]
        public static List<object> AppSortSides(List<object> list)
        {
            return AppModel.AppSortSides(list);
        }

        [Scriptable]
        public static List<object> AppSortAngles(List<object> list)
        {
            return AppModel.AppSortAngles(list);
        }
    }
}
