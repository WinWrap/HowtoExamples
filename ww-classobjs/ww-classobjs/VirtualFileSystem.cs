using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WinWrap.Basic;

namespace ww_classobjs
{
    public class VirtualFileSystem : IVirtualFileSystem
    {
        public string Combine(string baseScriptPath, string name)
        {
            // ignore baseScriptPath in this example
            // all scripts are in the same "directory"
            return name;
        }
        public void Delete(string scriptPath)
        {
            System.IO.File.Delete(ActualFileName(scriptPath));
        }
        public bool Exists(string scriptPath)
        {
            return System.IO.File.Exists(ActualFileName(scriptPath));
        }
        public string GetCaption(string scriptPath)
        {
            return scriptPath;
        }
        public DateTime GetTimeStamp(string scriptPath)
        {
            return System.IO.File.GetLastWriteTimeUtc(ActualFileName(scriptPath));
        }
        public string Read(string scriptPath)
        {
            return System.IO.File.ReadAllText(ActualFileName(scriptPath));
        }
        public void Write(string scriptPath, string text)
        {
            System.IO.File.WriteAllText(ActualFileName(scriptPath), text, System.Text.Encoding.UTF8);
        }
        private string ActualFileName(string scriptPath)
        {
            return Utils.MacroPath(scriptPath);
        }
    }
}