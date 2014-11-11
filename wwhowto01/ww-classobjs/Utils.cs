using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ww_classobjs
{
    public static class Utils
    {
        private static string DataDirectory()
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
        }

        public static string MacroPath(string name)
        {
            return string.Format(@"{0}\Macros\{1}", DataDirectory(), name);
        }

        public static string GetPatternString(string subdomain, string pattern)
        {
            // Exists on website only
            string path = string.Format(@"{0}\WinWrapBasic10\{1}-strings.txt",
                DataDirectory(), subdomain);
            if (!File.Exists(path)) return "00000000-0000-0000-0000-000000000000";
            string strings = File.ReadAllText(path);
            Regex rgx = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection matches = rgx.Matches(strings);
            string smatch = rgx.Match(strings).Groups[1].Value.ToString();
            return smatch;
        }

        public static string FormatError(WinWrap.Basic.Error error)
        {
            var sb = new StringBuilder();
            if (error != null)
            {
                if (File.Exists(error.File))
                {
                    string[] lines = File.ReadAllLines(error.File);
                    string line = lines[error.Line - 1];
                    if (error.Offset >= 0)
                        line = line.Insert(error.Offset, "!here!");
                    line = string.Format(@"* {0:00}: {1}", error.Line, line.Trim());
                    sb.AppendLine(line);
                    sb.AppendLine(error.Text);
                    sb.AppendLine("");
                    for (int i=1; i <= lines.Length; i++)
                    {
                        sb.AppendLine(string.Format(@"* {0:00}: {1}", i, lines[i-1]));
                    }
                }
            }
            return sb.ToString();
        }

        public static string FormatTimeoutError(WinWrap.Basic.BasicNoUIObj basicNoUIObj, bool timedout)
        {
            // get the line that's executing right now
            var sb = new StringBuilder();
            sb.AppendLine((string)basicNoUIObj.Query("GetStack")["Caller[0]"]);
            sb.AppendLine(timedout ? "time exceeded" : "paused" + ", terminating script.");
            sb.AppendLine("");
            return sb.ToString();
        }

    }
}