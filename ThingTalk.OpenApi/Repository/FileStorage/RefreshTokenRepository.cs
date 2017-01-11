using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using ThingTalk.OpenApi.Application.Domain;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.Repository.FileStorage
{
    /// <summary>
    /// 实现了IRefreshTokenRepository接口的仓库类
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private string _jsonFilePath;
        private List<RefreshToken> _refreshTokens;
        /// <summary>
        /// 构造函数：初始化RefreshToken数据
        /// </summary>
        public RefreshTokenRepository()
        {
            _jsonFilePath = HostingEnvironment.MapPath("~/App_Data/RefreshToken.json");
            if (File.Exists(_jsonFilePath))
            {
                var json = File.ReadAllText(_jsonFilePath);
                _refreshTokens = JsonConvert.DeserializeObject<List<RefreshToken>>(json);

            }
            if (_refreshTokens == null) _refreshTokens = new List<RefreshToken>();
        }
        /// <summary>
        /// 根据ID查找RefreshToken
        /// </summary>
        /// <param name="Id">RefreshToken ID</param>
        /// <returns></returns>
        public async Task<RefreshToken> FindById(string Id)
        {
            return _refreshTokens.Where(x => x.Id == Id).FirstOrDefault();
        }
        /// <summary>
        /// 通过用户名查找RefreshToken
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public RefreshToken FindByUserName(string UserName)
        {
            return _refreshTokens.Where(x => x.UserName == UserName).FirstOrDefault();
        }
        /// <summary>
        /// 插入RefreshToken
        /// </summary>
        /// <param name="refreshToken">RefreshToken实体</param>
        /// <returns></returns>
        public async Task<bool> Insert(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
            await WriteJsonToFile();
            return true;
        }
        /// <summary>
        /// 删除指定的RefreshToken
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string Id)
        {
            _refreshTokens.RemoveAll(x => x.Id == Id);
            await WriteJsonToFile();
            return true;
        }

        private async Task WriteJsonToFile()
        {
            using (var tw = TextWriter.Synchronized(new StreamWriter(_jsonFilePath, false)))
            {
                await tw.WriteAsync(JsonConvert.SerializeObject(_refreshTokens, Formatting.Indented));
            }
        }
    }
}