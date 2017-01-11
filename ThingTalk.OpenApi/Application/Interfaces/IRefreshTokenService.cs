using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ThingTalk.OpenApi.Application.Domain;

namespace ThingTalk.OpenApi.Application.Interfaces
{
    /// <summary>
    /// 与RefreshToken相关的服务接口
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// 在刷新access token时获取RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RefreshToken> Get(string Id);
        /// <summary>
        /// 通过用户名查找RefreshToken
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        RefreshToken FindByUserName(string UserName);
        /// <summary>
        /// 保存RefreshToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> Save(RefreshToken refreshToken);
        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Remove(string Id);
    }
}