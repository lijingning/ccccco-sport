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
        private User GetUser()
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["User"] = null;
            return RedirectToAction("Login");
        }
        // GET: User
        public ActionResult Index() //查看个人信息
        {
            User user = GetUser();
            return View(user);
        }

        // GET: User/Details/5
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
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                User user = new User { Name = viewModel.Name, Password = viewModel.Password, StuNum = viewModel.StuNum };
                user.Contact = viewModel.Contact ?? "";
                user.WeChatID = viewModel.WeChatID ?? "";
                user.SportTimeTable = new SportTimeTable();
                user.Groups = new List<Group>();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Gender,StuNum,Name,Contact")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
