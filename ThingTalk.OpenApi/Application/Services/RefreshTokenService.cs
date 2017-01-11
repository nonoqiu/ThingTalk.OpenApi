using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ThingTalk.OpenApi.Application.Domain;
using ThingTalk.OpenApi.Application.Interfaces;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.Application.Services
{
    /// <summary>
    /// 实现了IRefreshTokenService接口的服务类
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        /// <summary>
        /// IRefreshTokenRepository仓库实例
        /// </summary>
        private IRefreshTokenRepository _refreshTokenRepository;
        /// <summary>
        /// 使用IRefreshTokenRepository构造服务实例
        /// </summary>
        /// <param name="refreshTokenRepository"></param>
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        /// <summary>
        /// 在刷新access token时获取RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RefreshToken> Get(string Id)
        {
            return await _refreshTokenRepository.FindById(Id);
        }
        /// <summary>
        /// 通过用户名查找RefreshToken
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public RefreshToken FindByUserName(string UserName)
        {
            return _refreshTokenRepository.FindByUserName(UserName);
        }
        /// <summary>
        /// 保存RefreshToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> Save(RefreshToken refreshToken)
        {
            return await _refreshTokenRepository.Insert(refreshToken);
        }
        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string Id)
        {
            return await _refreshTokenRepository.Delete(Id);
        }
    }
}