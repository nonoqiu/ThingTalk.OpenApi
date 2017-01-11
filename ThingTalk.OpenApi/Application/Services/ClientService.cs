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
    /// 实现了IClientService接口的服务类
    /// </summary>
    public class ClientService : IClientService
    {
        /// <summary>
        /// IClientRepository仓库实例
        /// </summary>
        private IClientRepository _clientRepository;
        /// <summary>
        /// 使用IClientRepository构造服务实例
        /// </summary>
        /// <param name="clientRepository"></param>
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        /// <summary>
        /// 根据clientId获取相应的Client信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>        
        public async Task<Client> Get(string clientId)
        {
            return await _clientRepository.FindById(clientId);
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="deptCode">企业码</param>
        /// <returns></returns>
        public Account AccessCheck(string userName, string password, string deptCode)
        {
            return _clientRepository.AccessCheck(userName, password, deptCode);
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="deptCode">企业码</param>
        /// <param name="oldPsd">旧密码</param>
        /// <param name="newPsd">新密码</param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(string userName, string deptCode, string oldPsd, string newPsd)
        {
            return await _clientRepository.ChangePassword(userName, deptCode, oldPsd, newPsd);
        }
    }
}