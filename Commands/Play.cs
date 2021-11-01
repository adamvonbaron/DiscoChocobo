using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord.Commands;

namespace DiscoChocobo.Commands
{
    public class Play : DiscoChocoboCommandBase
    {
        private List<string> SupportedHosts { get; set; }

        public Play()
        {
            SupportedHosts = new List<string>(new string[]{
            "youtube.com",
            "youtu.be"
            });
        }

        private bool SupportedHostUrl(string url)
        {
            Uri uri;
            bool createResult;
            
            createResult = Uri.TryCreate(url, UriKind.Absolute, out uri);
            if (createResult && SupportedHosts.Contains(uri.Host))
                return true;
            else
                return false;
        }

        [Command("play")]
        [Summary
            ("play an audio file (place in queue)")]
        public async Task PlayAsync([Summary("url of audio")] string url)
        {
            if(!ValidUrl(url))
            {
                await ReplyAsync("cannot play file: invalid url (must be absolute)");
                return;
            }

            if (!SupportedHostUrl(url))
            {
                await ReplyAsync("cannot play file: unsupported host (type \"!play helphost\" to view supported hosts");
                return;
            }
        }
    }
}
