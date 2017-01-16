using System;

namespace ThingTalk.OpenApi.Application.Domain
{
    /// <summary>
    /// 通知信息实体
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public E_NOTIFY_TYPE Type { get; set; }
        /// <summary>
        /// 设备ID/手机号码
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 已读标记(1:已读,0:未读)
        /// </summary>
        public int ReadSign { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurTime { get; set; }
    }
}