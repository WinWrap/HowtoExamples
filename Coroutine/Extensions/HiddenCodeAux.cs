using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// Implements script access tokens for exclusive script access.
    /// </summary>
    public static class HiddenCodeAux
    {
        /// <summary>
        /// Get HiddenCode snippet defining the ScriptLock function.
        /// </summary>
        /// <param name="engineid">Script engine identifer.</param>
        /// <returns>HiddenCode snippet defining the ScriptLock function.</returns>
        /// <details>
        /// Sample script usage:
        /// <code>
        /// Using ScriptLock("port1")
        ///      ...
        /// End Using
        /// </code>
        /// Lock name "port1" is exclusive to this script engine during the using block.
        /// </details>
        public static string GetHiddenCodeSnippet(int engineid)
        {
            return string.Format(@"
Public Function ScriptLock(LockName As String) As System.IDisposable
    Dim accessref As Extensions.ScriptAccessRef = Extensions.HiddenCodeAux.GetScriptAccessRef({0}, LockName)
    While Not accessref.TryLock
        Wait .1
    End While
    Return accessref
End Function", engineid);
        }

        /// <summary>
        /// Master dictionary of all script access tokens.
        /// </summary>
        private static Dictionary<string, ScriptAccessToken> accesstokens_ = new Dictionary<string, ScriptAccessToken>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Get an access reference for a script engine.
        /// </summary>
        /// <param name="engineid">Script engine identifier.</param>
        /// <param name="lockname">Lock name.</param>
        /// <returns>A script access reference.</returns>
        public static ScriptAccessRef GetScriptAccessRef(int engineid, string lockname)
        {
            ScriptAccessToken accesstoken = null;
            if (!accesstokens_.TryGetValue(lockname, out accesstoken))
            {
                accesstoken = new ScriptAccessToken(lockname);
                accesstokens_.Add(lockname, accesstoken);
            }

            return new ScriptAccessRef(accesstoken, engineid);
        }

        /// <summary>
        /// Unlock all script access tokens for a script engine.
        /// </summary>
        /// <param name="engineid">Script engine identifier.</param>
        public static void UnlockAccessTokens(int engineid)
        {
            foreach (ScriptAccessToken accesstoken in accesstokens_.Values)
                accesstoken.UnlockAll(engineid);
        }

        /// <summary>
        /// Remove a script access token from the master dictionary.
        /// </summary>
        /// <param name="lockname">Lock name.</param>
        internal static void RemoveAccessToken(string lockname)
        {
            accesstokens_.Remove(lockname);
        }
    }
}
