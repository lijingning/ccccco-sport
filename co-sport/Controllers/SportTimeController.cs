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
            var sportTimes = user.SportTimes.ToList();
            //ICollection<SportTime> sportTimes = db.Users.Where(o => o.StuNum == user.StuNum).Single().SportTimes;
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

        [HttpPost]
        public ActionResult Index(string[] selectedTimes)
        {
            User user = GetUser();
            List<string> tempTimes;
            if (selectedTimes==null)
            {
                tempTimes=new List<string>();
            }
            else
            {
                tempTimes = new List<string>(selectedTimes);
            }
            var sportTimes = db.SportTimes;
            for (int i = 1; i <= 7; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    if (tempTimes.Contains(i.ToString() + j.ToString()))
                    {
                        if (user.SportTimes.Where(o => o.TimeID == i * 10 + j).FirstOrDefault() == null)
                        {
                            SportTime sportTime = new SportTime
                            {
                                SportTimeID = Guid.NewGuid(),
                                TimeID = i * 10 + j
                            };
                            user.SportTimes.Add(sportTime);
                        }
                    }
                    else
                    {
                        if (user.SportTimes.Where(o => o.TimeID == i * 10 + j).Count() > 0)
                        {
                            //user.SportTimes.Remove(user.SportTimes.Where(o => o.TimeID == i * 10 + j).Single());
                            sportTimes.Remove(user.SportTimes.Where(o => o.TimeID == i * 10 + j).Single());
                        }
                    }
                }
            }
            db.SaveChanges();

            TempData["Alert"] = "修改成功！";
            var paras = db.Users.Where(o => o.StuNum == user.StuNum).Single().SportTimes.ToList();
            bool[] selected = new bool[78];
            for (int i = 1; i <= 7; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    int temp = i * 10 + j;
                    selected[temp] = sportTimes.Where(o => o.TimeID == temp).Count() > 0 ? true : false;
                }
            }
            return View(selected);
        }
    }
}
