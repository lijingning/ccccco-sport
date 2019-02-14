using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using co_sport.Models;
using co_sport.Attributes;

namespace co_sport.Controllers
{
    [CheckLogin]
    public class SportTimeController : Controller
    {
        private SportContext db = new SportContext();

        private User GetUser()//已登录获取用户信息
        {
            string user_stuNum = Session["User"].ToString();
            User user = db.Users.Where(o => o.StuNum == user_stuNum).SingleOrDefault();
            return user;
        }

        public ActionResult Index()
        {
            User user = GetUser();
            ICollection<SportTime> sportTimes = db.Users.Where(o => o.StuNum == user.StuNum).Single().SportTimes;
            bool[] selected = new bool[78];
            for(int i=1; i<=7;i++)
            {
                for(int j=1;j<=7;j++)
                {
                    int temp = i * 10 + j;
                    selected[temp] = sportTimes.Where(o=>o.TimeID==temp).Count()>0 ? true : false;
                }
            }
            return View(selected);
        }
    }
}
