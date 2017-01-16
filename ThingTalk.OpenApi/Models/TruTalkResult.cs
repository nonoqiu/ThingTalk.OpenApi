using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThingTalk.OpenApi.Application.Domain;

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
        /// <summary>
        /// 自定义回复集合
        /// </summary>
        public IEnumerable<T> LstData { get; set; }
    }
}