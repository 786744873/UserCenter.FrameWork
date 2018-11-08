using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserCenter.IServices;
using UserCenter.OpenAPI.App_Start;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace UserCenter.OpenAPI.Controllers.v3
{
    public class JwtController : ApiController
    {
        public IUserService UserService { get; set; }
        public IGroupService GroupService { get; set; }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Login(string phoneNum, string password)
        {
            if (!await UserService.CheckLoginAsync(phoneNum, password))
            {
                return "手机号或密码错误！";
            }
            var secret = WebHelper.AppSetting();
            var user = await UserService.GetByPhoneNumAsync(phoneNum);
            var exprieDate = DateTime.UtcNow.AddMinutes(1);
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double expire = Math.Round((exprieDate - unixEpoch).TotalSeconds);

            var data = new Payload()
            {
                exp = expire,
                Id = user.Id,
                NickName = user.NickName,
                PhoneNum = user.PhoneNum
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(data, secret);
            return "得到 Token (有效期1分钟):" + token;
        }


        public async Task<IHttpActionResult> Test()
        {
            var groups = await GroupService.GetGroupsAsync(1);
            return Json(new { groups, DateTime = DateTime.Now }, WebHelper.CreateSerializerSettings());
        }
    }
}
