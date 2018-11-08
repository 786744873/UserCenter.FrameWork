/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.Services
/// 文件名称	：AppInfoService.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 13:19:19 
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

namespace UserCenter.Services.Services
{
    /// <summary>
    /// 
    /// <see cref="AppInfoService" langword="" />
    /// </summary>
    public class AppInfoService : BaseService<T_AppInfo>, IAppInfoService
    {
        protected override DbContext Db { get; set; }

        public AppInfoService(UserCenterContext context)
        {
            this.Db = context;
        }

        public async Task<long> AddNewAsync(string name, string appKey)
        {
            var isAny = await base.Entities.AnyAsync(a => a.AppKey == appKey);
            if (isAny)
            {
                throw new InvalidOperationException("该 AppKey 已存在");
            }
            var appSecret = Guid.NewGuid().ToString("N");
            var appInfo = new T_AppInfo()
            {
                CreateDate = DateTime.Now,
                AppKey = appKey,
                AppSecret = appSecret,
                Name = name,
                IsEnabled = false
            };
            base.Entities.Add(appInfo);
            return await this.Db.SaveChangesAsync();
        }


        public async Task<AppInfoDTO> GetByAppKeyAsync(string appKey)
        {
            var appInfo = await base.Entities.AsNoTracking().SingleOrDefaultAsync(a => a.AppKey == appKey && !a.IsEnabled);
            return ToDTO(appInfo);
        }

        AppInfoDTO ToDTO(T_AppInfo appInfo)
        {
            if (appInfo == null)
            {
                return null;
            }
            AppInfoDTO dto = new AppInfoDTO();
            dto.AppKey = appInfo.AppKey;
            dto.AppSecret = appInfo.AppSecret;
            dto.Id = appInfo.Id;
            dto.Name = appInfo.Name;
            dto.IsEnabled = appInfo.IsEnabled;
            return dto;
        }
    }
}
