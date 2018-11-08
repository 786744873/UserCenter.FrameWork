/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.DbContexts
/// 文件名称	：DbInitializer.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:45:17 
/// 邮箱		：786744873@qq.com
/// 个人主站	：https://www.cnblogs.com/wyt007/
///
/// 功能描述	：初始化数据
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
using UserCenter.IServices;

namespace UserCenter.Services
{
    /// <summary>
    /// 初始化数据
    /// <see cref="DbInitializer" langword="" />
    /// </summary>
    public class DbInitializer
    {
        public static async Task Initialize(IUserService userService, IGroupService groupService, IAppInfoService appInfoService)
        {
            if (await userService.UserExistsAsync("13057686866"))
            {
                return;
            }
            long userId = await userService.AddNewAsync("13057686866", "周星星", "123456");
            long userId2 = await userService.AddNewAsync("13057686867", "周润发", "123456");
            long userId3 = await userService.AddNewAsync("13057686868", "周杰伦", "123456");

            long groupId = await groupService.AddNewAsync("演员");
            long groupId2 = await groupService.AddNewAsync("导演");
            long groupId3 = await groupService.AddNewAsync("编剧");
            long groupId4 = await groupService.AddNewAsync("监制");
            long groupId5 = await groupService.AddNewAsync("歌手");

            await groupService.AddUserToGroupAsync(groupId, userId);
            await groupService.AddUserToGroupAsync(groupId2, userId);
            await groupService.AddUserToGroupAsync(groupId3, userId);
            await groupService.AddUserToGroupAsync(groupId4, userId);
            await groupService.AddUserToGroupAsync(groupId, userId2);
            await groupService.AddUserToGroupAsync(groupId5, userId3);

            await appInfoService.AddNewAsync("测试 Key", "testKey00000000");
        }
    }
}
