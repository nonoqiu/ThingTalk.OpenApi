using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using ThingTalk.OpenApi.Application.Domain;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.Repository.Interfaces
{
    /// <summary>
    /// INotifyRepository接口
    /// </summary>
    public interface INotifyRepository
    {
        /// <summary>
        /// 根据已读标记查找通知列表
        /// </summary>
        /// <param name="readSign">已读标记(1:已读,0:未读)</param>
        /// <returns></returns>
        IEnumerable<Notify> FindNotifyByReadSign(int readSign = 0);
        /// <summary>
        /// 设置已读标记
        /// </summary>
        /// <param name="notifyID">通知ID</param>
        /// <returns></returns>
        void SetNotifyReadSign(int notifyID);
    }
}