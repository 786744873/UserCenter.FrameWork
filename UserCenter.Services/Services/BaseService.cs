/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services
/// 文件名称	：BaseService.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 13:15:23 
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
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
    /// <summary>
    /// 
    /// <see cref="BaseService" langword="" />
    /// </summary>
    public abstract class BaseService<T> where T:BaseEntity
    {
        protected abstract DbContext Db { get; set; }

        protected DbSet<T> Entities => Db.Set<T>();

        public async Task<T> GetByIdAsync(long Id, bool isTracking = false)
        {
            if (isTracking)
            {
                return await Entities.SingleOrDefaultAsync(p => p.Id == Id);
            }
            return await Entities.AsNoTracking().SingleOrDefaultAsync(p => p.Id == Id);
        }
    }
}
