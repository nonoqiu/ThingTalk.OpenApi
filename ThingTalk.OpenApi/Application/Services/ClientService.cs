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
        /// 在刷新access token时获取RefreshToken
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>        
        public async Task<Client> Get(string clientId)
        {
            return await _clientRepository.FindById(clientId);
        }
    }
}