using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord.Commands;


namespace DiscoChocobo.Commands
{
    public class DiscoChocoboCommandBase : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Check if string provided is a valid HTTP or HTTPS URL
        /// </summary>
        /// <param name="url">The string to check</param>
        /// <returns>true if string is valid http(s) url, false if it is anything else</returns>
        protected static bool ValidUrl(string url)
        {
            Uri uri;
            bool createResult;

            createResult = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri);

            if (createResult && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
            else
                return false;
        }
    }
}
