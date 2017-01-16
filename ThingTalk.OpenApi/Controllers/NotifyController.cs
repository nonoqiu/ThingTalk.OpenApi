using System.Collections.Generic;
using System.Web.Http;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Providers;
using Newtonsoft.Json;
using System.Security.Claims;
using ThingTalk.OpenApi.Application.Interfaces;
using ThingTalk.OpenApi.Application.Services;
using ThingTalk.OpenApi.Repository.FileStorage;
using System.Threading.Tasks;
using ThingTalk.OpenApi.Application.Domain;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 通知相关接口
    /// </summary>
    //[Authorize]
    public class NotifyController : ApiController
    {
        //private IClientService _clientService;
        private INotifyRepository _notifyRepository;

        ///// <summary>
        ///// 构造函数：传入IRefreshTokenService接口实例
        ///// </summary>
        ///// <param name="refreshTokenService"></param>
        //public NotifyController(IRefreshTokenService refreshTokenService)
        //{
        //    _refreshTokenService = refreshTokenService;
        //    _clientService = new ClientService(new ClientRepository());
        //}

        public NotifyController()
        {
            _notifyRepository = new NotifyRepository();
        }

        /// <summary>
        /// 获取通知列表, 默认返回未读通知
        /// </summary>
        /// <param name="readSign">已读标记(1:已读,0:未读)</param>
        /// <returns>通知列表, 返回数据在LstData中</returns>
        [HttpGet]
        public TruTalkResult<Notify> GetNotifyList(int readSign = 0)
        {
            var type = "GetNotify";
            var lstData = _notifyRepository.FindNotifyByReadSign(readSign);
            var entity = new TruTalkResult<Notify>
            {
                BaseResult = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, type),
                LstData = lstData
            };
            return entity;
        }
        /// <summary>
        /// 置通知已读取标记
        /// </summary>
        /// <param name="notifyID">通知ID</param>        
        /// <returns>操作结果</returns>
        [HttpGet]
        public BaseResult ReadNotify(int notifyID)
        {
            _notifyRepository.SetNotifyReadSign(notifyID);

            var type = "ReadNotify";            
            var entity = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, type);
            return entity;
        }
        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="notifyID">通知ID</param>        
        /// <returns>操作结果</returns>
        [HttpGet]
        public BaseResult DeleteNotify(int notifyID)
        {
            var type = "DeleteNotify";
            var entity = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
            return entity;
        }
    }
}