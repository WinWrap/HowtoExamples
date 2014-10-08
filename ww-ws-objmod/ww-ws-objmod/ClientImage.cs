using System;

namespace ww_ws_objmod
{
    [Scriptable]
    public class ClientImage
    {
        [Scriptable]
        public event Action Started;

        public void Start(string firedby)
        {
            if (Started != null)
                Started();
        }

        [Scriptable]
        public string DrawLine()
        {
            return "asdf";
        }
    }
}