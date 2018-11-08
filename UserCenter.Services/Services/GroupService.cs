/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services
/// 文件名称	：GroupService.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 14:03:57 
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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.DbContexts;
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
    /// <summary>
    /// 
    /// <see cref="GroupService" langword="" />
    /// </summary>
    public class GroupService : BaseService<T_Group>, IGroupService
    {
        protected override DbContext Db { get; set; }

        public GroupService(UserCenterContext context)
        {
            this.Db = context;
        }

        public async Task<long> AddNewAsync(string name)
        {
            if (base.Entities.Any(g => g.Name == name))
            {
                throw new InvalidOperationException("该分组已存在！");
            }
            var group = new T_Group()
            {
                CreateDate = DateTime.Now,
                Name = name
            };
            this.Entities.Add(group);
            await this.Db.SaveChangesAsync();
            return group.Id;
        }

        public async Task<GroupDTO> GetByIdAsync(long id)
        {
            var group = await base.GetByIdAsync(id);
            return ToDTO(group);
        }

        public async Task<GroupDTO[]> GetGroupsAsync(long userId)
        {
            List<T_Group> groupList = await this.Db.Set<T_User>()
                .Include(nameof(T_User.UserGroup))
                .Include(nameof(T_UserGroup.Group))
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(t => t.UserGroup.Select(p => p.Group))
                .ToListAsync();

            return groupList.Select(t => ToDTO(t)).ToArray();
        }

        public async Task<UserDTO[]> GetGroupUsersAsync(long groupId)
        {
            List<T_User> userList = await this.Db.Set<T_Group>()
                .Include(nameof(T_Group.UserGroup))
                .Include(nameof(T_UserGroup.User))
                .AsNoTracking()
                .Where(g => g.Id == groupId)
                .SelectMany(t => t.UserGroup.Select(p => p.User))
                .ToListAsync();
            List<UserDTO> dtos = new List<UserDTO>();
            foreach (var user in userList)
            {
                UserDTO userDto = new UserDTO();
                userDto.Id = user.Id;
                userDto.NickName = user.NickName;
                userDto.PhoneNum = user.PhoneNum;
                dtos.Add(userDto);
            }
            return dtos.ToArray();
        }

        public async Task AddUserToGroupAsync(long groupId, long userId)
        {
            T_UserGroup userGroup = new T_UserGroup
            {
                UserId = userId,
                GroupId = groupId,
                CreateDate = DateTime.Now
            };
            this.Db.Set<T_UserGroup>().Add(userGroup);
            await this.Db.SaveChangesAsync();
        }

        public async Task RemoveUserFromGroupAsync(long groupId, long userId)
        {
            var userGroups = this.Db.Set<T_UserGroup>();
            var userGroup = await userGroups.SingleOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup != null)
            {
                userGroups.Remove(userGroup);
                await this.Db.SaveChangesAsync();
            }
        }

        GroupDTO ToDTO(T_Group group)
        {
            if (group == null)
            {
                return null;
            }
            GroupDTO dto = new GroupDTO();
            dto.Id = group.Id;
            dto.Name = group.Name;
            return dto;
        }
    }
}
