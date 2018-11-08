using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiController
    {
        public IUserService UserService { get; set; }

        /// <summary>
        /// 无惨 Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> Get()
        {
            bool exists = await UserService.UserExistsAsync("13057686866");
            return new[] { exists.ToString(), DateTime.Now.ToString() };
        }

        /// <summary>
        /// post 测试
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PostAsync(string phone)
        {
            var exists = await UserService.UserExistsAsync(phone);
            return phone + "---" + exists + "---" + DateTime.Now;
        }
    }
}
