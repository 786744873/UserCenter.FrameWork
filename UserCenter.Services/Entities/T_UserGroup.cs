/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Services.Entities
/// 文件名称	：T_UserGroup.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 11:17:45 
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
    /// <see cref="T_UserGroup" langword="" />
    /// </summary>
    public class T_UserGroup:BaseEntity
    {
        public long UserId { get; set; }

        public long GroupId { get; set; }



        public virtual T_User User { get; set; }

        public virtual T_Group Group { get; set; }

    }
}
