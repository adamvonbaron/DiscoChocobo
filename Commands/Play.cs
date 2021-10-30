using System;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace DiscoChocobo.Commands
{
    public class Play : DiscoChocoboCommandBase
    {

        [Command("play")]
        [Summary
            ("play an audio file (place in queue)")]
        public async Task PlayAsync([Summary("url of audio")] string url)
        {
            if(!ValidUrl(url))
            {
                await ReplyAsync("cannot play file: invalid url");
                return;
            }
        }
    }
}
