/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：UserCenter.OpenAPI
/// 文件名称    ：VersionControllerSelector.cs
/// =================================
/// 创 建 者    ：wyt
/// 创建日期    ：2018/11/8 16:35:11 
/// 邮箱        ：786744873@qq.com
/// 个人主站    ：https://www.cnblogs.com/wyt007/
/// 功能描述    ：版本选择
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
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace UserCenter.OpenAPI
{
    /// <summary>
    /// API版本选择
    /// <see cref="VersionControllerSelector" langword="" />
    /// </summary>
    public class VersionControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration config;

        /// <summary>
        /// API版本选择
        /// </summary>
        /// <param name="configuration"></param>
        public VersionControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            config = configuration;
        }

        static Dictionary<string, HttpControllerDescriptor> dic = new Dictionary<string, HttpControllerDescriptor>();

        /// <summary>
        /// 获取所有的controller键值集合
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {

            //获取当前程序集所有控制器
            var cTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract&& typeof(ApiController).IsAssignableFrom(t)&& t.Name.EndsWith("Controller"));

            Regex versionRegex = new Regex(@"\w+\.(v[0-9]+)", RegexOptions.IgnoreCase);
            foreach (var type in cTypes)
            {
                int cIndex = type.Name.IndexOf("Controller");
                var cName = type.Name.Substring(0, cIndex).ToUpper();
                var match = versionRegex.Match(type.FullName);
                string key;
                if (match.Success)
                {
                    key = cName + match.Groups[1].Value.ToUpper();
                }
                else
                {
                    key = cName + "V1";
                }
                dic[key] = new HttpControllerDescriptor(config, type.Name, type);
            }
            return dic;
        }

        /// <summary>
        /// 选择需要按版本请求的Controller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //获取路由数据
            if (request.GetRouteData().Values.TryGetValue("controller", out var value))
            {
                var match = Regex.Match(request.RequestUri.AbsoluteUri, @"/(v[0-9]+)/", RegexOptions.IgnoreCase);
                string key;
                if (match.Success)
                {
                    key = value.ToString().ToUpper() + match.Groups[1].Value.ToUpper();
                }
                else
                {
                    key = value.ToString().ToUpper() + "v1";
                }
                if (dic.TryGetValue(key, out var cDescriptor))
                {
                    return cDescriptor;
                }
            }

            return base.SelectController(request);
        }
    }
}