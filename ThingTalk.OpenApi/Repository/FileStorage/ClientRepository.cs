using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using ThingTalk.OpenApi.Application.Domain;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.Repository.FileStorage
{
    /// <summary>
    /// 实现了IClientRepository接口的仓库类
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private static readonly Client[] _clients;

        static ClientRepository()
        {
            var json = File.ReadAllText(HostingEnvironment.MapPath("~/App_Data/clients.json"));
            _clients = JsonConvert.DeserializeObject<Client[]>(json);
        }
        /// <summary>
        /// 查找指定ID的Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Client> FindById(string id)
        {
            return _clients.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}