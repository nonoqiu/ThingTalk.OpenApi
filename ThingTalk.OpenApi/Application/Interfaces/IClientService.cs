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
    }
}