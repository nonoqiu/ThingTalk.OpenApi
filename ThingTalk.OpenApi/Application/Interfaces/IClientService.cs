using System.Threading.Tasks;
using ThingTalk.OpenApi.Application.Domain;

namespace ThingTalk.OpenApi.Application.Interfaces
{
    /// <summary>
    /// 与Client相关的服务接口
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// 通过clientId获取客户端信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<Client> Get(string clientId);
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