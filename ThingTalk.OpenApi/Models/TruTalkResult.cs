using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Models
{
    /// <summary>
    /// 信云消息回复
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TruTalkResult<T> where T : class
    {
        /// <summary>
        /// 回复基类
        /// </summary>
        public BaseResult BaseResult { get; set; }
        /// <summary>
        /// 自定义回复类
        /// </summary>
        public T Data { get; set; }
    }
}