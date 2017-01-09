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
    public class OAuthClientTest
    {
        private HttpClient _httpClient;

        public OAuthClientTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:17328/");
        }

        //[Fact]
        /// <summary>
        /// 调用与用户无关的Web API，直接使用client_credentials进行登录
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Get_Accesss_Token_By_Client_Credentials_Grant()
        {
            Console.WriteLine(await GetAccessTokenTest(true));
        }

        // 不使用用户名和密码获取token(无效)
        [TestMethod]
        public async Task TestGetValue()
        {
            Console.WriteLine(await (await _httpClient.GetAsync("/api/values")).Content.ReadAsStringAsync());
        }
        //[Fact]
        // 使用用户名和密码获取token
        [TestMethod]
        public async Task Call_WebAPI_By_Access_Token()
        {
            var token = await GetAccessToken();
            //var token = "WKN3uL8nepfyLFPm6giC2loHJ5yfGsi_N0aFG5v-uwiAuxccdq0gR0__VH4M6lbUNnwViOKIw_l93a4cDN8FKhUHtUWchgGZx9D16hMwuqr8DltwAWhJWvDbVju89klzwFg2yA90mzSo_pGABr4Iy8-FK5pld0rHvmfEvHb-7Z23TDoi-Lcyaynj2HttUcLhVbmTIgEADq8MgK3jJhpntQ";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine(await (await _httpClient.GetAsync("/api/values")).Content.ReadAsStringAsync());
        }

        private async Task<string> GetAccessToken()
        {
            var clientId = "1234";
            var clientSecret = "5678";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "client_credentials");

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            return JObject.Parse(responseValue)["access_token"].Value<string>();
        }

        /// <summary>
        /// 调用与用户相关的Web API, 使用 用户名 和 密码 进行登录
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        //[Fact]
        public async Task Get_Accesss_Token_By_Resource_Owner_Password_Credentials_Grant()
        {
            Console.WriteLine(await GetAccessTokenTest(false));
        }

        private async Task<string> GetAccessTokenTest(bool bUseClient_credentials)
        {
            var clientId = "1234";
            var clientSecret = "5678";

            var parameters = new Dictionary<string, string>();
            if (bUseClient_credentials)
            {
                parameters.Add("grant_type", "client_credentials");
            }
            else
            {
                parameters.Add("grant_type", "password");               // 使用用户名和密码进行身份验证验证
                parameters.Add("username", "trutalk,270806");
                parameters.Add("password", "tyt");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));
            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            Console.WriteLine("responseValue: " + responseValue);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseValue)["access_token"].Value<string>();
            }
            else
            {
                Console.WriteLine(responseValue);
                return string.Empty;
            }
        }
        
        [TestMethod]
        public async Task Call_WebAPI_By_Resource_Owner_Password_Credentials_Grant()
        {
            var token = await GetAccessTokenTest(true);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine(await (await _httpClient.GetAsync("/api/users/current")).Content.ReadAsStringAsync());
        }
    }
}