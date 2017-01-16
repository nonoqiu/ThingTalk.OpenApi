using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ThingTalk.OpenApi.Application.Domain
{
    /// <summary>
    /// 通知信息实体
    /// </summary>
    public class TruTalkMsg<T> where T : class
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 自定义回复集合
        /// </summary>
        public IEnumerable<T> LstData { get; set; }

        /// <summary>
        /// 通知内容(JSON序列化的LstData)
        /// </summary>
        public string MsgData
        {
            get
            {
                return JsonConvert.SerializeObject(LstData);
            }
        }
    }
}