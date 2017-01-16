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

namespace ThingTalk.OpenApi.Repository.FileStorage
{
    /// <summary>
    /// 实现了INotifyRepository接口的仓库类
    /// </summary>
    public class NotifyRepository : INotifyRepository
    {
        private static List<Notify> _lstNotify;

        private static readonly string[] _devices = new string[] { "17339567129", "18003449733", "17339567266" };
        private static readonly string[] _contents = new string[] { "RFID白名单,RFID标签文件,RFID配置", "亲情号码配置, 定位参数, 发布服务号码配置", "接收到新文件" };

        static NotifyRepository()
        {
            _lstNotify = new List<Notify>();
            _lstNotify.Add(new Notify() { ID = 1, DeviceName = _devices[0], OccurTime = DateTime.Now, Content = _contents[0], DeviceID = _devices[0], ReadSign = 0, Type = E_NOTIFY_TYPE.TYPE_FILE_UPLOAD });
            _lstNotify.Add(new Notify() { ID = 2, DeviceName = _devices[1], OccurTime = DateTime.Now, Content = _contents[1], DeviceID = _devices[1], ReadSign = 0, Type = E_NOTIFY_TYPE.TYPE_PARAMS_DOWNLOAD });
            _lstNotify.Add(new Notify() { ID = 3, DeviceName = _devices[2], OccurTime = DateTime.Now, Content = _contents[2], DeviceID = _devices[2], ReadSign = 0, Type = E_NOTIFY_TYPE.TYPE_PARAMS_DOWNLOAD });
        }

        /// <summary>
        /// 根据已读标记查找通知列表
        /// </summary>
        /// <param name="readSign">已读标记(1:已读,0:未读)</param>
        /// <returns></returns>
        public IEnumerable<Notify> FindNotifyByReadSign(int readSign = 0)
        {
            return _lstNotify.Where(x => x.ReadSign == readSign);
        }
        /// <summary>
        /// 设置已读标记
        /// </summary>
        /// <param name="notifyID">通知ID</param>
        /// <returns></returns>
        public void SetNotifyReadSign(int notifyID)
        {
            var notify = _lstNotify.Where(m => m.ID == notifyID).FirstOrDefault();
            if (notify != null) notify.ReadSign = 1;
        }
    }
}