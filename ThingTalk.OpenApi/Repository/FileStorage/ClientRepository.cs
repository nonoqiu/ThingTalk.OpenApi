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
        private static readonly Account[] _accounts;

        static ClientRepository()
        {
            var json = File.ReadAllText(HostingEnvironment.MapPath("~/App_Data/clients.json"));
            _clients = JsonConvert.DeserializeObject<Client[]>(json);

            var jsonAccount = File.ReadAllText(HostingEnvironment.MapPath("~/App_Data/account.json"));
            _accounts = JsonConvert.DeserializeObject<Account[]>(jsonAccount);
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

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="deptCode">企业码</param>
        /// <returns></returns>
        public Account AccessCheck(string userName, string password, string deptCode)
        {
            return _accounts.Where(m => m.UserName.Equals(userName) && m.Password.Equals(password) && m.DeptCode.Equals(deptCode)).FirstOrDefault();            
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
            var entity = _accounts.Where(m => m.UserName.Equals(userName) && m.Password.Equals(oldPsd) && m.DeptCode.Equals(deptCode)).FirstOrDefault();
            if (entity != null)
            {
                entity.Password = newPsd;
                await WriteJsonToFile();
                return true;
            }
            return false;
        }
        private async Task WriteJsonToFile()
        {
            var _jsonFilePath = HostingEnvironment.MapPath("~/App_Data/account.json");
            using (var tw = TextWriter.Synchronized(new StreamWriter(_jsonFilePath, false)))
            {
                await tw.WriteAsync(JsonConvert.SerializeObject(_accounts, Formatting.Indented));
            }
        }
    }
}