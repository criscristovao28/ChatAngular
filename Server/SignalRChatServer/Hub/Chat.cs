using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace SignalRChatServer
{
    public class Chat: Hub
    {
        public static List<Message> Messages { get; set; }


        public Chat()
        {
            if (Messages == null) Messages = new List<Message>();
        }
        public void NewMessage(string userName, string message)
        {
            Clients.All.SendAsync("NewMessage", userName, message);
            Messages.Add(new Message()
            {
                Text = message,
                UserName = userName
            }) ;            
        }

        public void NewUser(string userName, string conectionID)
        {
            Clients.Client(conectionID).SendAsync("previousMessages", Messages);
            Clients.All.SendAsync("newUser", userName);
        }


        public class Message
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }
    }
}
