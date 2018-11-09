/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：UserCenter.OpenAPI.App_Start
/// 文件名称    ：AuthorizationFilter.cs
/// =================================
/// 创 建 者    ：wyt
/// 创建日期    ：2018/11/8 17:28:26 
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

using JWT;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using UserCenter.Common;
using UserCenter.DTO;
using UserCenter.IServices;

namespace UserCenter.OpenAPI.App_Start
{
    /// <summary>
    /// 
    /// <see cref="AuthorizationFilter" langword="" />
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private IAppInfoService _appInfoService;
        public AuthorizationFilter(IAppInfoService appInfoService)
        {
            this._appInfoService = appInfoService;
        }

        public bool AllowMultiple => true;

        Regex version = new Regex(@"\.v(\d+)$");

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var attrs = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            if (attrs.Count == 1)
            {
                return await continuation();
            }

            attrs = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            if (attrs.Count == 1)
            {
                return await continuation();
            }

            var headers = actionContext.Request.Headers;

            var cNameSpace = actionContext.ControllerContext.ControllerDescriptor.ControllerType.Namespace;
            var match = version.Match(cNameSpace);

            if (match.Success && float.TryParse(match.Groups[1].Value, out var v) && v > 2)
            {
                if (!headers.TryGetValues("JWT", out var jwt))
                {
                    return Content(HttpStatusCode.Unauthorized, "JWT为空");
                }


                try
                {
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    IJwtValidator validator = new JwtValidator(serializer, provider);
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                    var secret = WebHelper.AppSetting();
                    var data = decoder.DecodeToObject<Payload>(jwt.FirstOrDefault(), secret, true);
                    return await continuation();
                }
                catch (TokenExpiredException)
                {
                    return Content(HttpStatusCode.Unauthorized, "Token 已过期");
                }
                catch (SignatureVerificationException)
                {
                    return Content(HttpStatusCode.Unauthorized, "签名错误！");
                }
            }
            if (!headers.TryGetValues("AppKey", out var appKeys))
            {
                return Content(HttpStatusCode.Unauthorized, "AppKey为空");
            }
            if (!headers.TryGetValues("Sign", out var signs))
            {
                return Content(HttpStatusCode.Unauthorized, "Sign为空");
            }
            string appkey = appKeys.FirstOrDefault();
            string sign = signs.FirstOrDefault();

            var appInfo = await _appInfoService.GetByAppKeyAsync(appkey);
            if (appInfo == null)
            {
                return Content(HttpStatusCode.Unauthorized, "AppKey错误");
            }

            var paramArr = actionContext.Request
                  .GetQueryNameValuePairs()
                  .OrderBy(kv => kv.Key)
                  .Select(kv => kv.Key + "=" + kv.Value)
                  .ToArray();

            string sign2 = MD5Helper.ToMD5(string.Join("&", paramArr) + appInfo.AppSecret);
            if (!sign.Equals(sign2,StringComparison.InvariantCultureIgnoreCase))
            {
                return Content(HttpStatusCode.Unauthorized, "签名错误");
            }
            return await continuation();
        }

        HttpResponseMessage Content(HttpStatusCode statusCode, string content)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content)
            };
        }
    }

    public class Payload : UserDTO
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public double exp { get; set; }
    }
}