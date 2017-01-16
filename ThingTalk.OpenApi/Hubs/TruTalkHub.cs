using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Threading.Tasks;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Application.Domain;
using Newtonsoft.Json;
using System;

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
        /// 推送消息
        /// </summary>
        /// <param name="to">推送对象</param>
        /// <param name="msgType">消息类别</param>
        /// <param name="msgData">消息内容</param>
        public void pushTruTalkMsg(string to, string msgType, string msgData)
        {
            Clients.All.pushTruTalkMsg(msgType, msgData);
        }

        private static int iIndex = 5;
        private static readonly string[] _devices = new string[] { "17339567129", "18003449733", "17339567266" };
        private static readonly string[] _contents = new string[] { "RFID白名单,RFID标签文件,RFID配置", "亲情号码配置, 定位参数, 发布服务号码配置", "接收到新文件" };
        /// <summary>
        /// 创建通知
        /// </summary>
        /// <returns></returns>
        private TruTalkMsg<Notify> CreateMessage()
        {
            var lstNotify = new List<Notify>();
            var iType = new Random().Next(1, 100) % 3;
            var notify = new Notify()
            {
                ID = iIndex++,
                DeviceID = _devices[new Random().Next(1, 100) % 3],
                OccurTime = DateTime.Now,
                ReadSign = 0,
            };

            switch (iType)
            {
                case 0:
                    notify.Content = _contents[2];
                    notify.Type = E_NOTIFY_TYPE.TYPE_FILE_UPLOAD;
                    break;
                case 1:
                    notify.Content = _contents[new Random().Next(1, 100) % 2];
                    notify.Type = E_NOTIFY_TYPE.TYPE_PARAMS_DOWNLOAD;
                    break;
                default:
                    return null;
            }
            notify.DeviceName = notify.DeviceID;
            lstNotify.Add(notify);
            return new TruTalkMsg<Notify>() { MsgType = "Notify", LstData = lstNotify };            
        }

        /// <summary>
        /// 绑定新用户
        /// </summary>
        public void bind(string refresh_token)
        {
            User u = new User()
            {
                Name = refresh_token,
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
        private static int recordsToBeProcessed = 10000000;
        private void PushMessage(object o)
        {
            for (int record = 0; record <= recordsToBeProcessed; record++)
            {
                var to = record % 10 == 0 ? "0" : "1";
                //sendTo(to, string.Format("Processing item {0} of {1}", record, recordsToBeProcessed));

                var msg = CreateMessage();
                if (msg == null) continue;
                pushTruTalkMsg(to, msg.MsgType, msg.MsgData);
                Thread.Sleep(3000);
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