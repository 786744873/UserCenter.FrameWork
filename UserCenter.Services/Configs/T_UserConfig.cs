/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.Configs
/// 文件名称	：T_UserConfig.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:26:09 
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
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Entities;

namespace UserCenter.Services.Configs
{
    /// <summary>
    /// 
    /// <see cref="T_UserConfig" langword="" />
    /// </summary>
    public class T_UserConfig : EntityTypeConfiguration<T_User>
    {
        public T_UserConfig()
        {
            ToTable(nameof(T_User) + "s");
            HasKey(t => t.Id);
            Property(p => p.CreateDate).HasColumnType("datetime").IsRequired();

            Property(p => p.PhoneNum).HasMaxLength(16).IsRequired();
            Property(p => p.NickName).HasMaxLength(64).IsRequired();
            Property(p => p.PasswordHash).HasMaxLength(256).IsUnicode(false).IsRequired();
            Property(p => p.PasswordSalt).IsRequired().HasMaxLength(32).IsUnicode(false);
        }
    }
}
