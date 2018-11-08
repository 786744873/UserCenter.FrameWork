/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.DbContexts
/// 文件名称	：UserCenterContext.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:40:47 
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserCenter.Services.Entities;

namespace UserCenter.Services.DbContexts
{
    /// <summary>
    /// 
    /// <see cref="UserCenterContext" langword="" />
    /// </summary>
    public class UserCenterContext:DbContext
    {
        public UserCenterContext():base("default")
        {
            //EF初始化模式为空
            Database.SetInitializer(new CreateDatabaseIfNotExists<UserCenterContext>());
            this.Database.Log = (sql) => {
                System.Diagnostics.Debug.Write($"EF执行SQL：{sql}");
            };
        }

        public DbSet<T_User> Users { get; set; }
        public DbSet<T_AppInfo> AppInfos { get; set; }
        public DbSet<T_Group> Groups { get; set; }
        public DbSet<T_UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
