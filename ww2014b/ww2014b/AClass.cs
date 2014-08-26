using System;

namespace ww2014b
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