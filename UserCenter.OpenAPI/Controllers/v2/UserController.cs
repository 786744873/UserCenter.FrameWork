using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Controllers.v2
{
    /// <summary>
    /// WebApi 版本 v2
    /// </summary>
    [AllowAnonymous]
    public class UserController : ApiController
    {
        public IUserService UserService { get; set; }

        /// <summary>
        /// test -- v2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test()
        {
            return DateTime.Now.ToString() + "---v2---";
        }

        /// <summary>
        /// 新增用户 -- v2
        /// </summary>
        /// <param name="phoneNum">v2 手机号码</param>
        /// <param name="nickName">v2 昵称</param>
        /// <param name="password">v2 密码</param>
        /// <returns>id long</returns>
        [HttpPost]
        public async Task<long> AddNew(string phoneNum, string nickName, string password)
        {
            return await UserService.AddNewAsync(phoneNum, nickName, password);
        }
    }
}
