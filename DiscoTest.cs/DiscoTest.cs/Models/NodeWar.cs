using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoTest.cs.Models
{
    public class NodeWar
    {
        public List<SocketUser> signups;
        public NodeWarDate nodeWarDate;

        public NodeWar(int day, int month)
        {
            this.signups = new List<SocketUser>();
            this.nodeWarDate = new NodeWarDate(day, month);
        }
    }
}
