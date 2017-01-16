using System;

namespace ThingTalk.OpenApi.Application.Domain
{
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum E_NOTIFY_TYPE
    {
        ///// <summary>
        ///// 紧急报警通知
        ///// </summary>
        //TYPE_SOS_ALARM = 1001,
        ///// <summary>
        ///// 滞留报警通知
        ///// </summary>
        //TYPE_SOS_RETENTION = 1002,

        /// <summary>
        /// 文件上传通知
        /// </summary>        
        TYPE_FILE_UPLOAD = 1001,
        /// <summary>
        /// 参数下载通知
        /// </summary>
        TYPE_PARAMS_DOWNLOAD = 1002
    }
}