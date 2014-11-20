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
        public static ClientImage TriangleImage
        {
            get
            {
                return AppModel.TriangleImage;
            }
            set
            {
                AppModel.TriangleImage = value;
            }
        }

        [Scriptable]
        public static double SideA
        {
            get
            {
                return AppModel.SideA;
            }
            set
            {
                AppModel.SideA = value;
            }
        }

        [Scriptable]
        public static double SideB
        {
            get
            {
                return AppModel.SideB;
            }
            set
            {
                AppModel.SideB = value;
            }
        }

        [Scriptable]
        public static double SideC
        {
            get
            {
                return AppModel.SideC;
            }
            set
            {
                AppModel.SideC = value;
            }
        }

        [Scriptable]
        public static double AngleA
        {
            get
            {
                return AppModel.AngleA;
            }
            set
            {
                AppModel.AngleA = value;
            }
        }

        [Scriptable]
        public static double AngleB
        {
            get
            {
                return AppModel.AngleB;
            }
            set
            {
                AppModel.AngleB = value;
            }
        }

        [Scriptable]
        public static double AngleC
        {
            get
            {
                return AppModel.AngleC;
            }
            set
            {
                AppModel.AngleC = value;
            }
        }
    }
}
