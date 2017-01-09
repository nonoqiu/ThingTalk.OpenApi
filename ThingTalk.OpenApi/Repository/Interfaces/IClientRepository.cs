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
    }
}