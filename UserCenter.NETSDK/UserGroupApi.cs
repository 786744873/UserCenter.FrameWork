/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.NETSDK
/// 文件名称	：UserGroupApi.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/9 10:04:37 
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
    /// <see cref="UserGroupApi" langword="" />
    /// </summary>
    public class UserGroupApi
    {
        private string appKey;
        private string appSecret;
        private string serverRoot;
        public UserGroupApi(string appKey, string appSecret, string serverRoot)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverRoot = serverRoot;
        }

        public async Task<UserGroup> GetByIdAsync(long id)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["id"] = id;
            var result = await client.GetAsync("UserGroup/GetById", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<UserGroup>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetById 失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<UserGroup[]> GetGroupsAsync(long userId)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["userId"] = userId;
            var result = await client.GetAsync("UserGroup/GetGroups", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<UserGroup[]>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetGroups 失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task<User[]> GetGroupUsersAsync(long userGroupId)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["userGroupId"] = userGroupId;
            var result = await client.GetAsync("UserGroup/GetGroupUsers", data);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
                return JsonConvert.DeserializeObject<User[]>(result.Result);
            }
            else
            {
                throw new ApplicationException("GetGroupUsers 失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }
        public async Task AddUserToGroupAsync(long userGroupId, long userId)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["userGroupId"] = userGroupId;
            data["userId"] = userId;
            var result = await client.GetAsync("UserGroup/AddUserToGroup", data);
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
            }
            else
            {
                throw new ApplicationException("AddUserToGroup 失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }

        public async Task RemoveUserFromGroupAsync(long userGroupId, long userId)
        {
            SDKClient client = new SDKClient(appKey, appSecret, serverRoot);
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["userGroupId"] = userGroupId;
            data["userId"] = userId;
            var result = await client.GetAsync("UserGroup/RemoveUserFromGroup", data);
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                //因为返回的报文体是新增id：{5}
                //使用newtonsoft把json格式反序列化为long
            }
            else
            {
                throw new ApplicationException("RemoveUserFromGroup 失败，状态码"
                    + result.StatusCode + "，响应报文" + result.Result);
            }
        }
    }
}
