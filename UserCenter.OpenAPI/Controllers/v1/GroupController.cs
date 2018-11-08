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
    /// 分组管理
    /// </summary>
    [AllowAnonymous]
    public class GroupController : ApiController
    {
        public IGroupService GroupService { get; set; }
        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> AddNew(string name)
        {
            return "新增成功，Id=" + await GroupService.AddNewAsync(name);
        }
        /// <summary>
        /// 将用户添加到指定分组
        /// </summary>
        /// <param name="groupId">分组id</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> AddUserToGroup(long groupId, long userId)
        {
            await GroupService.AddUserToGroupAsync(groupId, userId);
            return true;
        }
        /// <summary>
        /// 根据分组id获取分组信息
        /// </summary>
        /// <param name="id">分组id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GroupDTO> GetById(long id)
        {
            return await GroupService.GetByIdAsync(id);
        }
        /// <summary>
        /// 根据用户id获取分组信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GroupDTO[]> GetGroups(long userId)
        {
            return await GroupService.GetGroupsAsync(userId);
        }
        /// <summary>
        /// 根据分组id获取该组用户信息
        /// </summary>
        /// <param name="groupId">分组id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDTO[]> GetUsers(long groupId)
        {
            return await GroupService.GetGroupUsersAsync(groupId);
        }
        /// <summary>
        /// 移除分组中的用户
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> RemoveGroupUser(long groupId, long userId)
        {
            await GroupService.RemoveUserFromGroupAsync(groupId, userId);
            return true;
        }
    }
}
