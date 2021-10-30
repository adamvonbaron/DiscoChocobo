using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace DiscoChocobo.Commands
{
    public class UserInfo : DiscoChocoboCommandBase
    {
        [Command("userInfo")]
        [Summary
            ("Returns info about the current user, or the user param, if one is passed")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("optional user param")] SocketUser user = null)
        {
            SocketUser userInfo;
            
            userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
