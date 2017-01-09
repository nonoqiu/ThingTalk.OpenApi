﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ThingTalk.OpenApi.Application.Domain;

namespace ThingTalk.OpenApi.Repository.Interfaces
{
    /// <summary>
    /// 与RefreshToken相关的仓库接口
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// 查找RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RefreshToken> FindById(string Id);
        /// <summary>
        /// 新增RefreshToken
        /// </summary>
        /// <param name="refreshToken">RefreshToken实体</param>
        /// <returns></returns>
        Task<bool> Insert(RefreshToken refreshToken);
        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Delete(string Id);
    }
}