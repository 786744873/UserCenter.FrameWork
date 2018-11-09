using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCenter.NETSDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserApi user = new UserApi("testKey00000000", "d95842e96ce84a10b76bc6dc4d36e45f", "http://localhost:37755/api/v1/");


            var u = user.GetByIdAsync(1).Result;
            Console.WriteLine(u.Id+" "+u.NickName+" "+u.PhoneNum+" ");

            //var id = user.AddNewAsync("13872303747", "Aun", "qqqqq").Result;
            // Console.WriteLine(id+" ");

            //var res = user.CheckLoginAsync("13272303747", "qqqqq").Result;
            // Console.WriteLine(res + " ");

            //var u = user.GetByPhoneNumAsync("13272303747").Result;
            //Console.WriteLine(u.Id + " " + u.NickName + " " + u.PhoneNum + " ");


            //UserGroupApi userGroup = new UserGroupApi("qqqqq", "12345", "http://127.0.0.1:8888/api/v1/");
            // userGroup.AddUserToGroupAsync(2, 3).Wait();

            //var userGroupArr = userGroup.GetGroupsAsync(3).Result;
            //foreach (var ug in userGroupArr)
            //{
            //    Console.WriteLine(ug.Id+" Name:"+ug.Name);
            //}

            //var ug1 = userGroup.GetByIdAsync(2).Result;
            //Console.WriteLine("Group信息：" + ug1.Id + " UserGroupName:" + ug1.Name);

            //Console.WriteLine("该Group中的User：");

            //var userArr = userGroup.GetGroupUsersAsync(2).Result;
            //foreach (var u in userArr)
            //{
            //    Console.WriteLine(u.Id + " Name:" + u.NickName + "  " + u.PhoneNum);
            //}

            //userGroup.RemoveUserFromGroupAsync(2, 3).Wait();
            //Console.WriteLine("删除成功");

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
