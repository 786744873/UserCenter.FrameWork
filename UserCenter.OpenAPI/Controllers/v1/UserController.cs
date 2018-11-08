using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.DTO;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.Controllers.v1
{
    /// <summary>
    /// WebApi 版本 v1
    /// </summary>
    public class UserController : ApiController
    {
        public IUserService UserService { get; set; }

        /// <summary>
        /// 新增用户 -- v1
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="nickName">昵称</param>
        /// <param name="password">密码</param>
        /// <returns>id long</returns>
        [HttpPost]
        public async Task<string> AddNew(string phoneNum, string nickName, string password)
        {
            return "新增成功，Id=" + await UserService.AddNewAsync(phoneNum, nickName, password);
        }
        /// <summary>
        /// test -- v1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test()
        {
            return DateTime.Now.ToString() + "---v1---";
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <param name="phoneNum">手机</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CheckLogin(string phoneNum, string password)
        {
            return await UserService.CheckLoginAsync(phoneNum, password);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDTO> GetById(long id)
        {
            return await UserService.GetByIdAsync(id);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="phoneNum">手机</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDTO> GetByPhoneNum(string phoneNum)
        {
            return await UserService.GetByPhoneNumAsync(phoneNum);
        }
        /// <summary>
        /// 检查该手机号码是否注册
        /// </summary>
        /// <param name="phoneNum">手机</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UserExists(string phoneNum)
        {
            return await UserService.UserExistsAsync(phoneNum);
        }
    }
}
