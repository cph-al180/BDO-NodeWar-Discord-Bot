using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscoTest.cs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscoTest.cs.Modules
{
    public class NodeWarRegistry : ModuleBase<SocketCommandContext>
    {

        [Command("newnw")]
        public async Task NewNodeWar(int day, int month)
        {
            NodeWarRepository.nodeWar = new NodeWar(day, month);
            await ReplyAsync("Created new Node War at date: " + NodeWarRepository.nodeWar.nodeWarDate.day + "/" + NodeWarRepository.nodeWar.nodeWarDate.month);
        }

        [Command("signup")]
        public async Task SignUp()
        {

            if(NodeWarRepository.nodeWar.signups.Count > 0)
            {
                foreach (SocketUser signup in NodeWarRepository.nodeWar.signups)
                {
                    if (signup == Context.User)
                    {
                        await ReplyAsync("You are already signed up, " + GetNickname(Context.User));
                    }
                    else
                    {
                        NodeWarRepository.nodeWar.signups.Add(Context.User);
                        await ReplyAsync("Signed you up for Node War " + GetNickname(Context.User) + "!");
                    }
                }
            }
            else
            {
                NodeWarRepository.nodeWar.signups.Add(Context.User);
                await ReplyAsync("Signed you up for Node War " + GetNickname(Context.User) + "!");
            }
        }

        [Command("signoff")]
        public async Task SignOff()
        {
            if (NodeWarRepository.nodeWar.signups.Count > 0)
            {
                bool found = false;
                int index = 0;
                foreach (SocketUser signup in NodeWarRepository.nodeWar.signups)
                {
                    if (signup == Context.User)
                    {
                        found = true;
                        NodeWarRepository.nodeWar.signups.RemoveAt(index);
                        await ReplyAsync("You have been removed from sign ups, " + GetNickname(Context.User));
                    }
                    index++;
                }
                if(found == false)
                {
                    await ReplyAsync("Seems like you didn't sign up in the first place, " + GetNickname(Context.User));
                }
            }
            else
            {
                await ReplyAsync("Nobody has even signed up yet.. and you want to sign off?.. ");
            }
        }

        [Command("nw")]
        public async Task GetNodeWar()
        {
            await ReplyAsync("Next scheduled Node War is at: " + NodeWarRepository.nodeWar.nodeWarDate.day + "/" + NodeWarRepository.nodeWar.nodeWarDate.month);
        }

        [Command("viewsignups")]
        public async Task SeeSignUps()
        {
            string message = "";
            if(NodeWarRepository.nodeWar.signups.Count == 0)
            {
                await ReplyAsync("No sign ups so far");
            }
            else
            {
                foreach (SocketUser signup in NodeWarRepository.nodeWar.signups)
                {
                    message += "\n" + GetNickname(signup);
                }
                await ReplyAsync("Sign ups:");
                await ReplyAsync(message);
            }
        }

        private string GetNickname(SocketUser user)
        {
            return Context.Guild.GetUser(Context.User.Id).Nickname;
        }
    }
}
