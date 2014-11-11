using System.Collections.Generic;

namespace ScriptingModel
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
        public static void AppTrace(string msg)
        {
            AppModel.AppTrace(msg);
        }

    }
}
