using System;
using Microsoft.AspNet.SignalR;

namespace ThingTalk.OpenApi.Hubs
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            Clients.All.push(name, message);
            Clients.All.addNewMessageToPage(name, message);

            Console.WriteLine("Now connected, connection ID=" + Context.ConnectionId);

            //Clients.Client("ddd").push(name, message);
            //Clients.Client("ddd").addNewMessageToPage(name, message);
        }
    }
}