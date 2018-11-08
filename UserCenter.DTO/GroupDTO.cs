/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.DTO
/// 文件名称	：UserGroupDTO.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 10:27:47 
/// 邮箱		：786744873@qq.com
/// 个人主站	：https://www.cnblogs.com/wyt007/
///
/// 功能描述	：用户组 传输对象
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

namespace UserCenter.DTO
{
    /// <summary>
    /// 用户组 传输对象
    /// <see cref="GroupDTO" langword="" />
    /// </summary>
    public class GroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
