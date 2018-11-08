/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services
/// 文件名称	：UserService.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 13:35:55 
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
using UserCenter.Common;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.DbContexts;
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
    /// <summary>
    /// 
    /// <see cref="UserService" langword="" />
    /// </summary>
    public class UserService : BaseService<T_User>, IUserService
    {
        protected override DbContext Db { get; set; }

        public UserService(UserCenterContext context)
        {
            this.Db = context;
        }

        public async Task<long> AddNewAsync(string phoneNum, string nickName, string password)
        {
            var isAny = await UserExistsAsync(phoneNum);
            if (isAny)
            {
                throw new InvalidOperationException("无效操作，该手机号码已注册！");
            }
            var salt = Guid.NewGuid().ToString("N").ToUpper();
            var user = new T_User()
            {
                CreateDate = DateTime.Now,
                NickName = nickName,
                PasswordHash = CreatePwd(password, salt),
                PasswordSalt = salt,
                PhoneNum = phoneNum
            };
            base.Entities.Add(user);
            await this.Db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> CheckLoginAsync(string phoneNum, string password)
        {
            var user =await base.Entities.AsNoTracking().SingleOrDefaultAsync(u => u.PhoneNum == phoneNum);
            if (user==null)
            {
                return false;
            }
            return CreatePwd(password, user.PasswordSalt) == user.PasswordHash;
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            var user = await base.GetByIdAsync(id);
            return ToDTO(user);
        }

        public async Task<UserDTO> GetByPhoneNumAsync(string phoneNum)
        {
            var user = await base.Entities.AsNoTracking().SingleOrDefaultAsync(u => u.PhoneNum == phoneNum);
            return ToDTO(user);
        }

        public async Task<bool> UserExistsAsync(string phoneNum)
        {
            return await base.Entities.AnyAsync(u => u.PhoneNum == phoneNum);
        }

        private string CreatePwd(string sourcePwd, string salt)
        {
            if (string.IsNullOrEmpty(sourcePwd) || string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("参数不能为空！");
            }

            return MD5Helper.ToMD5(sourcePwd+salt);
        }

        UserDTO ToDTO(T_User user)
        {
            if (user == null)
            {
                return null;
            }
            UserDTO dto = new UserDTO();
            dto.Id = user.Id;
            dto.NickName = user.NickName;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }
    }
}
