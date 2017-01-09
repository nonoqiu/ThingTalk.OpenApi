using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Tests.Controllers
{
    [TestClass]
    public class RefreshTokenTest
    {
        private HttpClient _httpClient;

        public RefreshTokenTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8006/");
            //_httpClient.BaseAddress = new Uri("http://localhost:17328/");
        }
        /// <summary>
        /// 测试客户端获取 refresh token
        /// 客户端获取 access token 与 refresh token 是一起的        
        /// </summary>
        /// <returns>
        /// {   "access_token":"LszeYCqVrpmbS...",
        ///     "token_type":"bearer",
        ///     "expires_in":1209599,
        ///     "refresh_token":"4d1df676aabe4a40a63fb92dbf24e13d"
        /// }
        /// </returns>
        [TestMethod]
        public async Task GetAccessTokenTest()
        {
            //var clientId = "1234";
            //var clientSecret = "5678";
            var clientId = "trutalk";
            var clientSecret = "111222333444555";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "client_credentials");
            parameters.Add("username", "博客园团队");
            parameters.Add("password", "cnblogs.com");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseValue);
        }
        /// <summary>
        /// 测试客户端用 refresh token 刷新 access token
        /// 这段客户端代码与前一步中客户端代码的主要区别是少了下面传递 resource owner 用户名与密码的代码，
        /// 这就是 refresh token 的用途所在 —— 不需要用户名与密码就可以刷新 access token。
        /// </summary>
        /// <returns>
        /// {   "access_token":"OsjHn_SDcQEbpB...",
        ///     "token_type":"bearer",
        ///     "expires_in":1209599,
        ///     "refresh_token":"914242df60e04a408c81b361d06b3418"
        ///     }
        /// </returns>
        [TestMethod]
        public async Task GetAccessTokenByRefreshTokenTest()
        {
            var clientId = "1234";
            var clientSecret = "5678";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", "4d1df676aabe4a40a63fb92dbf24e13d");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseValue);
        }
    }
}