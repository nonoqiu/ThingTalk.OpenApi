using System.Threading.Tasks;
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
        /// 通过用户名查找RefreshToken
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        RefreshToken FindByUserName(string UserName);
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