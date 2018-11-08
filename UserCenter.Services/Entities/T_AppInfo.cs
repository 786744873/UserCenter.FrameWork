/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services
/// 文件名称	：T_AppInfo.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:15:29 
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
    /// <see cref="T_AppInfo" langword="" />
    /// </summary>
    public class T_AppInfo:BaseEntity
    {
        public string Name { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public bool IsEnabled { get; set; }
    }
}
