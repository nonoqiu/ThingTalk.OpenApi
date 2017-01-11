using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ThingTalk.OpenApi.Application.Domain;

namespace ThingTalk.OpenApi.Repository.Interfaces
{
    /// <summary>
    /// 与Client相关的仓库接口
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// 查找Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> FindById(string id);
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="deptCode">企业码</param>
        /// <returns></returns>
        Account AccessCheck(string userName, string password, string deptCode);
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="deptCode">企业码</param>
        /// <param name="oldPsd">旧密码</param>
        /// <param name="newPsd">新密码</param>
        /// <returns></returns>
        Task<bool> ChangePassword(string userName, string deptCode, string oldPsd, string newPsd);
    }
}