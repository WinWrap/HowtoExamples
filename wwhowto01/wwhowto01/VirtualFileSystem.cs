﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
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
            //System.IO.File.Delete(ActualFileName(scriptPath));
        }
        public bool Exists(string scriptPath) // xxx when used?
        {
            var request = (HttpWebRequest)WebRequest.Create(ActualFileName(scriptPath));
            request.Method = "HEAD";
            var response = (HttpWebResponse)request.GetResponse();
            var success = response.StatusCode == HttpStatusCode.OK;
            return success;
            //return System.IO.File.Exists(ActualFileName(scriptPath));
        }
        public string GetCaption(string scriptPath)
        {
            return scriptPath;
        }
        public DateTime GetTimeStamp(string scriptPath) // xxx used when?
        {
            var webRequest = WebRequest.Create(ActualFileName(scriptPath));
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            return webResponse.LastModified;
            //return System.IO.File.GetLastWriteTimeUtc(ActualFileName(scriptPath));
        }
        public string Read(string scriptPath)
        {
            var webRequest = WebRequest.Create(ActualFileName(scriptPath));
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content)) 
            {
                return reader.ReadToEnd(); // xxx
            }
            //return System.IO.File.ReadAllText(ActualFileName(scriptPath));
        }
        public void Write(string scriptPath, string text)
        {
            //System.IO.File.WriteAllText(ActualFileName(scriptPath), text, System.Text.Encoding.UTF8);
        }
        private string ActualFileName(string scriptPath)
        {
            //return Utils.MacroPath(scriptPath);
            return string.Format(@"https://raw.githubusercontent.com/WinWrap/HowtoExamples/master/wwhowto01/wwhowto01/App_Data/Macros/{0}", scriptPath);
        }
    }
}