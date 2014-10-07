using System;

namespace ww_ws_objmod
{
    [Scriptable]
    public class AClass
    {
        [Scriptable]
        public event Action Started;

        public void Start(string firedby)
        {
            if (Started != null)
                Started();
        }
    }
}