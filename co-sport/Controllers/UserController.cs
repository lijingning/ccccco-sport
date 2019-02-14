using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using co_sport.Models;
using co_sport.ViewModels;
using co_sport.Attributes;
using System.Web.Security;

namespace co_sport.Controllers
{
    [CheckLogin]
    public class UserController : Controller
    {
        private SportContext db = new SportContext();
        private User GetUser()//已登录获取用户信息
        {
            string user_stuNum =Session["User"].ToString();
            User user = db.Users.Where(o => o.StuNum == user_stuNum).SingleOrDefault();
            return user;
        }

        [AllowAnonymous]
        public ActionResult Login()//登陆
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            User user = db.Users.Where(o => o.StuNum == viewModel.StuNum).SingleOrDefault();

            if (user==null || user.Password!=viewModel.Password)
            {
                ViewBag.Pass = false;
                viewModel.Password = "";
                return View(viewModel);
            }
            Session["User"] = viewModel.StuNum;
            return RedirectToAction("Index");
        }

        public ActionResult Logout()//登出
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["User"] = null;
            TempData["Alert"] = "注销成功！";
            return RedirectToAction("Login");
        }

        public ActionResult Index() //查看个人信息
        {
            User user = GetUser();
            return View(user);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Pass = true;
            ViewBag.Existence = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if(viewModel.Password!=viewModel.PasswordConfirmed)
            {
                viewModel.Password = "";
                viewModel.PasswordConfirmed = "";
                ViewBag.Pass = false;
                ViewBag.Existence = false;
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                if(db.Users.Count(o=>o.StuNum==viewModel.StuNum)>0)
                {
                    ViewBag.Existence = true;
                    ViewBag.Pass = true;
                    return View();
                }
                User user = new User { Name = viewModel.Name, Password = viewModel.Password, StuNum = viewModel.StuNum };
                user.Contact = viewModel.Contact ?? "";
                user.WeChatID = viewModel.WeChatID ?? "";
                user.Groups = new List<Group>();
                user.SportTimes = new List<SportTime>();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Edit()
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,StuNum,Name,Contact,WeChatID")] User _user)
        {
            User user = GetUser();
            user.Contact=_user.Contact;
            user.WeChatID = _user.WeChatID;
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                TempData["Alert"] = "数据储存出现错误，请联系系统管理员！";
            }
            TempData["Alert"] = "修改成功！";
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.Pass = false;
            ViewBag.Correct = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            ViewBag.Pass = false;
            ViewBag.Correct = false;
            if(viewModel.NewPassword!=viewModel.NewPasswordConfirmed)
            {
                ViewBag.Pass = true;
                return View();
            }
            User user= GetUser();
            if(viewModel.OriginalPassword!=user.Password)
            {
                ViewBag.Correct = true;
                return View();
            }
            Session["User"] = null;
            user.Password = viewModel.NewPassword;
            db.SaveChanges();
            TempData["Alert"] = "修改密码成功！";
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
