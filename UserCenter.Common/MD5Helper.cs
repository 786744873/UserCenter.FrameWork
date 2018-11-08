/// ***********************************************************************
///
/// =================================
/// CLR版本	：4.0.30319.42000
/// 命名空间	：UserCenter.Common
/// 文件名称	：MD5Helper.cs
/// =================================
/// 创 建 者	：wyt
/// 创建日期	：2018/11/8 13:53:42 
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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.Common
{
    /// <summary>
    /// 
    /// <see cref="MD5Helper" langword="" />
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// 将字符串转换成md5
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToMD5(string source)
        {
            using (MD5 md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public static string ToMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(stream);
                StringBuilder sb = new StringBuilder(32);
                foreach (var item in hashBytes)
                {
                    sb.Append(item.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string ToMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder(32);
                foreach (var item in hashBytes)
                {
                    sb.Append(item.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
