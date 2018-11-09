/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.NETSDK
/// 文件名称	：UserApi.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/9 10:00:29 
/// 邮箱		：786744873@qq.com
/// 个人主站	：https://www.cnblogs.com/wyt007/
///
/// 功能描述	：
/// 使用说明	：
/// =================================
/// 修改者	：
/// 修改日期	：
/// 修改内容	：
/// =================================
///
/// ***********************************************************************


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NETSDK
{
    /// <summary>
    /// 
    /// <see cref="UserApi" langword="" />
    /// </summary>
    public class UserApi
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserApi(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }

        public async Task<long> AddNewAsync(string phoneNum, string nickName, string password)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["phoneNum"] = phoneNum;
            data["nickName"] = nickName;
            data["password"] = password;
            var result = await client.GetAsync("User/AddNew", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<long>(result.Result);
            }
            else
            {
                throw new ApplicationException("AddNew失败，状态码"+ result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<bool> UserExistsAsync(string phoneNum)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["phoneNum"] = phoneNum;
            var result = await client.GetAsync("User/UserExists", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<bool>(result.Result);
            }
            else
            {
                throw new ApplicationException("UserExists失败，状态码"+ result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<bool> CheckLoginAsync(string phoneNum, string password)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["phoneNum"] = phoneNum;
            data["password"] = password;
            var result = await client.GetAsync("User/CheckLogin", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<bool>(result.Result);
            }
            else
            {
                throw new ApplicationException("CheckLogin 失败，状态码"+ result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["id"] = id;
            var result = await client.GetAsync("User/GetById", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<User>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetById失败，状态码"+ result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<User> GetByPhoneNumAsync(string phoneNum)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["phoneNum"] = phoneNum;
            var result = await client.GetAsync("User/GetByPhoneNum", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<User>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetByPhoneNum 失败，状态码"+ result.StatusCode + "，响应报文" + result.Result);
            }
        }
    }
}
