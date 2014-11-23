using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using WinWrap.Basic;
using System.Text.RegularExpressions;

namespace ww_classobjs
{
    public class VirtualFileSystem : IVirtualFileSystem
    {
        private bool local_ = true;

        public string Combine(string baseScriptPath, string name)
        {
            // ignore baseScriptPath in this example
            // all scripts are in the same "directory"
            return name;
        }
        public void Delete(string scriptPath)
        {
            //System.IO.File.Delete(ActualFileName(scriptPath));
        }
        public bool Exists(string scriptPath) // xxx when used?
        {
            if (IsLocalScript(scriptPath))
            {
                return System.IO.File.Exists(ActualFileName(scriptPath));
            }
            else
            {
                var request = (HttpWebRequest)WebRequest.Create(ActualFileName(scriptPath));
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                var success = response.StatusCode == HttpStatusCode.OK;
                return success;
            }
        }
        public string GetCaption(string scriptPath)
        {
            return scriptPath;
        }
        public DateTime GetTimeStamp(string scriptPath) // xxx used when?
        {
            if (IsLocalScript(scriptPath))
            {
                return System.IO.File.GetLastWriteTimeUtc(ActualFileName(scriptPath));
            }
            else
            {
                var webRequest = WebRequest.Create(ActualFileName(scriptPath));
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                return webResponse.LastModified;
            }
        }
        public string Read(string scriptPath)
        {
            if (IsLocalScript(scriptPath))
            {
                return System.IO.File.ReadAllText(ActualFileName(scriptPath));
            }
            else
            {
                var webRequest = WebRequest.Create(ActualFileName(scriptPath));
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    return reader.ReadToEnd(); // xxx
                }
            }
        }
        public void Write(string scriptPath, string text)
        {
            //System.IO.File.WriteAllText(ActualFileName(scriptPath), text, System.Text.Encoding.UTF8);
        }
        private bool IsLocalScript(string scriptPath)
        {
            string pat = @"(.*)/([^/]+)";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(scriptPath);
            string scriptDir = m.Groups[1].ToString();
            return scriptDir.Equals("local");
        }
        private string ScriptName(string scriptPath)
        {
            string pat = @"(.*)/([^/]+)";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(scriptPath);
            string scriptName = m.Groups[2].ToString();
            return scriptName;
        }
        private string ActualFileName(string scriptPath)
        {
            if (IsLocalScript(scriptPath))
            {
                return Utils.MacroPath(ScriptName(scriptPath));
            }
            else
            {
                //return string.Format(@"https://raw.githubusercontent.com/WinWrap/HowtoExamples/master/wwhowto01/wwhowto01/App_Data/Macros/{0}", scriptPath);
                return scriptPath;
            }
        }
    }
}