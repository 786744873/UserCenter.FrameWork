/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：UserCenter.OpenAPI.Filters
/// 文件名称    ：ExceptionFilter.cs
/// =================================
/// 创 建 者    ：wyt
/// 创建日期    ：2018/11/8 15:04:14 
/// 邮箱        ：786744873@qq.com
/// 个人主站    ：https://www.cnblogs.com/wyt007/
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace UserCenter.OpenAPI.Filters
{
    /// <summary>
    /// 
    /// <see cref="ExceptionFilter" langword="" />
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        public bool AllowMultiple => true;

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            using (StreamWriter writer = File.AppendText("~/err.txt"))
            {
                await writer.WriteLineAsync(actionExecutedContext.Exception.ToString());
            }
        }
    }
}