using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscoTest.cs.Modules
{
    public class Utility : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingPong()
        {
            await ReplyAsync("Pong!");
        }

        [Command("info")]
        public async Task Info()
        {
            await ReplyAsync("Hello, I am Vampz-NW-Bot. " +
                "\nI was created to assist in the management of guild member signups for Node Wars in Black Desert Online, by Taki." +
                "\nUse the !help command to see what I am capable of! :)");
        }

        [Command("help")]
        public async Task Help()
        {
            await ReplyAsync("Commands: " +
                "\n- !newnw {day} {month} : Creates a new Node War event (Admins only) -- Caution!! This will overwrite the current Node War event" +
                "\n- !signup : Signs you up for the up and coming Node War" +
                "\n- !signoff : Signs you off of the up and coming Node War" +
                "\n- !nw : Shows the date of the up and coming Node War" +
                "\n- !viewsignups : Shows the names of all currently signed up guild members");
        }
    }
}
