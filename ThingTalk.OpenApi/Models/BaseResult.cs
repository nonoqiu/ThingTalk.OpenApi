using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Models
{
    /// <summary>
    /// 消息回复基类
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 消息编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类别信息
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }
}