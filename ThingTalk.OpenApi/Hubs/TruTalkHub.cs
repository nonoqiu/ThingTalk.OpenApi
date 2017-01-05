using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Hubs
{
    /// <summary>
    /// Trutalk推送中心
    /// </summary>
    public class TruTalkHub : Hub
    {
        private static List<User> users = new List<User>();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void send(string name, string message)
        {
            Clients.All.push(name, message);
        }
        /// <summary>
        /// 向指定用户发送消息
        /// </summary>        
        /// <param name="to"></param>
        /// <param name="message"></param>
        public void sendTo(string to, string message)
        {
            var uList = users.Where(u => u.Name.Contains(to));
            foreach (var user in uList)
            {
                if (user != null) Clients.Client(user.Id).pushOne("server notify", message);
            }
        }
        /// <summary>
        /// 增加新用户
        /// </summary>
        public void bind(string userName)
        {
            User u = new User()
            {
                Name = userName,
                Id = Context.ConnectionId
            };
            users.Add(u);
            //Clients.All.bindList(users);
            //Clients.Client(u.Id).setName(u.Name);

            if (!bRun)
            {
                bRun = true;
                ThreadPool.QueueUserWorkItem(new WaitCallback(PushMessage));
            }
        }
        private static bool bRun { get; set; }
        private static int recordsToBeProcessed = 100000;
        private void PushMessage(object o)
        {
            for (int record = 0; record <= recordsToBeProcessed; record++)
            {
                var to = record % 10 == 0 ? "0" : "1";
                sendTo(to, string.Format("Processing item {0} of {1}", record, recordsToBeProcessed));
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        /// <summary>
        /// 客户端断开
        /// </summary>
        /// <returns></returns>
        //public override Task OnDisconnected(bool stopCalled)        
        public override Task OnDisconnected()
        {
            users.Remove(users.Where(u => u.Id == Context.ConnectionId).SingleOrDefault());
            return base.OnDisconnected();
        }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
    }
}