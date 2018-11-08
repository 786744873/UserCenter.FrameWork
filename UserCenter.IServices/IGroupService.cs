/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.IServices
/// 文件名称	：IUserGroupService.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 10:45:27 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;

namespace UserCenter.IServices
{
    /// <summary>
    /// 
    /// <see cref="IGroupService" langword="" />
    /// </summary>
    public interface IGroupService:IServiceTag
    {
        Task<long> AddNewAsync(string name);

        Task<GroupDTO> GetByIdAsync(long id);

        Task<GroupDTO[]> GetGroupsAsync(long userId);

        Task<UserDTO[]> GetGroupUsersAsync(long groupId);


        Task AddUserToGroupAsync(long groupId, long userId);

        Task RemoveUserFromGroupAsync(long groupId, long userId);
    }
}
