/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：UserCenter.OpenAPI.App_Start
/// 文件名称    ：WebHelper.cs
/// =================================
/// 创 建 者    ：wyt
/// 创建日期    ：2018/11/8 17:18:57 
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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UserCenter.OpenAPI.App_Start
{
    /// <summary>
    /// 
    /// <see cref="WebHelper" langword="" />
    /// </summary>
    public class WebHelper
    {
        static JsonSerializerSettings settings = null;

        public static JsonSerializerSettings CreateSerializerSettings()
        {
            if (settings != null)
            {
                return settings;
            }
            settings = new JsonSerializerSettings();

            //将C#中的大写开头的属性转换为小写开头 
            DefaultContractResolver resolver1 = new DefaultContractResolver();
            resolver1.NamingStrategy = new CamelCaseNamingStrategy();
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//json序列化是默认的时间格式
            settings.ContractResolver = resolver1;
            //忽略反序列化时不在Class中的对象
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.MaxDepth = 32;//递归层次32次
            settings.TypeNameHandling = TypeNameHandling.None;

            return settings;
        }

        public static string AppSetting(string key=null)
        {
            return ConfigurationManager.AppSettings["jwtKey"];
        }
    }
}