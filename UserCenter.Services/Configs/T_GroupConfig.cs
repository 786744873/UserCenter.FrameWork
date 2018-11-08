/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.Configs
/// 文件名称	：T_GroupConfig.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:28:02 
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
    /// <see cref="T_GroupConfig" langword="" />
    /// </summary>
    public class T_GroupConfig : EntityTypeConfiguration<T_Group>
    {
        public T_GroupConfig()
        {
            ToTable(nameof(T_Group) + "s");
            HasKey(t => t.Id);
            Property(p => p.CreateDate).HasColumnType("datetime").IsRequired();

            Property(p => p.Name).HasMaxLength(32).IsRequired();
        }
    }
}
