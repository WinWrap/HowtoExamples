using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// Reference to a script access token.
    /// </summary>
    public class ScriptAccessRef : IDisposable
    {
        /// <summary>
        /// Script engine identifier.
        /// </summary>
        internal int EngineId { get; private set; }

        /// <summary>
        /// Script access token referenced.
        /// </summary>
        private ScriptAccessToken accesstoken_;

        /// <summary>
        /// Create a script access reference.
        /// </summary>
        /// <param name="accesstoken">Script access token referenced.</param>
        /// <param name="engineid">Script engine identifier.</param>
        internal ScriptAccessRef(ScriptAccessToken accesstoken, int engineid)
        {
            accesstoken_ = accesstoken;
            EngineId = engineid;
            // add this script access reference to the script access token's list
            accesstoken_.AddAccessRef(this);
        }

        /// <summary>
        /// Try to lock a script access token.
        /// </summary>
        /// <returns>True, if the script access token has been successfully locked.</returns>
        public bool TryLock()
        {
            return accesstoken_.TryLock(EngineId);
        }

        /// <summary>
        /// Unlock the script access token, if it has been locked by this script access reference.
        /// </summary>
        public void Dispose()
        {
            accesstoken_.Unlock(this);
        }
    }
}
