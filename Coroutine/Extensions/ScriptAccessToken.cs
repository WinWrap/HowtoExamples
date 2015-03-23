using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// Implementation of script access token.
    /// </summary>
    internal class ScriptAccessToken
    {
        /// <summary>
        /// Unlocked when zero.
        /// Locked when non-zero and contains the script engine identifier.
        /// </summary>
        internal int EngineId { get; private set; }

        /// <summary>
        /// Lock name.
        /// </summary>
        private string lockname_;

        /// <summary>
        /// All script access references to this script access token.
        /// </summary>
        private List<ScriptAccessRef> accessrefs_ = new List<ScriptAccessRef>();

        /// <summary>
        /// Create a script access token.
        /// </summary>
        /// <param name="lockname">Lock name.</param>
        internal ScriptAccessToken(string lockname)
        {
            lockname_ = lockname;
        }

        /// <summary>
        /// Add a script access reference.
        /// </summary>
        /// <param name="accessref">Script access reference.</param>
        internal void AddAccessRef(ScriptAccessRef accessref)
        {
            accessrefs_.Add(accessref);
        }

        /// <summary>
        /// Try to lock the script access token.
        /// </summary>
        /// <param name="engineid">Script engine identifier.</param>
        /// <returns>True, if script access token is locked by the script engine.</returns>
        public bool TryLock(int engineid)
        {
            if (EngineId == 0)
            {
                // script access token can be locked
                EngineId = engineid;
            }

            return EngineId == engineid;
        }

        /// <summary>
        /// Unlock the script access token if it is only locked by this script access reference.
        /// </summary>
        /// <param name="accessref">A script access reference to this script access token.</param>
        public void Unlock(ScriptAccessRef accessref)
        {
            accessrefs_.Remove(accessref);
            if (accessref.EngineId == EngineId && !accessrefs_.Exists(ar => ar.EngineId == EngineId))
            {
                // unlock this script access token because it was locked by this script access reference
                // and is not accessed by any other script access references from the same script engine
                EngineId = 0;
            }

            if (accessrefs_.Count == 0)
            {
                // script access token is no longer needed
                HiddenCodeAux.RemoveAccessToken(lockname_);
            }
        }

        /// <summary>
        /// Unlock all script access tokens for a script engine.
        /// </summary>
        /// <param name="engineid">Script engine identifier.</param>
        public void UnlockAll(int engineid)
        {
            // create temp list of script access references for this script engine
            List<ScriptAccessRef> accessrefs = new List<ScriptAccessRef>(accessrefs_.Where(accessref => accessref.EngineId == engineid));
            // unlock each script access reference for this script engine
            foreach (ScriptAccessRef accessref in accessrefs)
                Unlock(accessref);
        }
    }
}
