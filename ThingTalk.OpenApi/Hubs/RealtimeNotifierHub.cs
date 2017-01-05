using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace ThingTalk.OpenApi.Hubs
{
    public class RealtimeNotifierHub : Hub
    {
        public int recordsToBeProcessed = 1000;

        public void DoLongOperation()
        {
            for (int record = 0; record <= recordsToBeProcessed; record++)
            {
                if (ShouldNotifyClient(record))
                {
                    // 向调用者发送消息
                    // Clients.Caller.sendMessage(string.Format("Processing item {0} of {1}", record, recordsToBeProcessed));

                    // 向所有人发送消息
                    Clients.All.sendMessage(string.Format("Processing item {0} of {1}", record, recordsToBeProcessed));
                    Thread.Sleep(10);
                }
            }
        }

        private static bool ShouldNotifyClient(int record)
        {
            return record % 10 == 0;
        }
    }
}