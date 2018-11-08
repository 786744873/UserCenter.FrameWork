/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.Entities
/// 文件名称	：T_User.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:16:46 
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

namespace UserCenter.Services.Entities
{
    /// <summary>
    /// 
    /// <see cref="T_User" langword="" />
    /// </summary>
    public class T_User:BaseEntity
    {
        public string PhoneNum { get; set; }

        public string NickName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }



        public virtual ICollection<T_UserGroup> UserGroup { get; set; } = new HashSet<T_UserGroup>();
    }
}
