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
using co_sport.ViewModels;

namespace co_sport.Controllers
{
    [CheckLogin]
    public class GroupController : Controller
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
            return View();
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            string[,] table = new string[8,7];
            foreach(var user in group.Users)
            {
                foreach(var sportTime in user.SportTimes)
                {
                    int i = sportTime.TimeID / 10, j = sportTime.TimeID % 10;
                    table[i, j] += user.Name + " ";
                }
            }
            DetailsViewModel detailsViewModel = new DetailsViewModel();
            detailsViewModel.Group = group;
            detailsViewModel.Table = table;
            return View(detailsViewModel);
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup([Bind(Include = "GroupID,Name,Abstract")] Group group)
        {
            User user = GetUser();
            if (ModelState.IsValid)
            {
                group.GroupID = Guid.NewGuid();
                group.Manager = GetUser();
                group.Users = new List<User>();
                group.Count = 1;
                db.Groups.Add(group);
                user.Groups.Add(group);
                db.SaveChanges();
                TempData["Alert"] = "创建成功！";
                return RedirectToAction("Index");
            }
            return View(group);
        }

        public ActionResult MyGroup()
        {
            var viewModel = new List<GroupViewModel>();
            User user = GetUser();
            foreach(Group group in user.Groups)
            {
                var temp = new GroupViewModel {
                    Abstract = group.Abstract,
                    Count = group.Count,
                    GroupName = group.Name,
                    GroupID = group.GroupID,
                    IsManager = user.StuNum == group.Manager.StuNum ? true : false
                };
                viewModel.Add(temp);
            }
            return View(viewModel);
        }

        public ActionResult JoinGroup()
        {
            User user = GetUser();
            List<Group> Groups = new List<Group>();
            foreach (Group g in db.Groups.ToList())
            {
                if (!g.Users.Contains(user))
                {
                    Groups.Add(g);
                }
            }
            return View(Groups);
        }
        
        public ActionResult ApplyForJoining(Guid GroupID)
        {
            User user = GetUser();
            Group group = db.Groups.Find(GroupID);

            Request request = db.Requests.Where(o => o.GroupID == GroupID && o.StuNum == user.StuNum && o.Agreed==null).SingleOrDefault();

            if(request!=null)
            {
                TempData["Alert"] = "你已经发送过申请了，请等待管理员审批！";
                return RedirectToAction("JoinGroup");
            }
            request = new Request
            {
                RequestID = Guid.NewGuid(),
                Agreed = null,
                StuNum = user.StuNum,
                StuName=user.Name,
                GroupID = group.GroupID
            };
            db.Requests.Add(request);
            db.SaveChanges();
            TempData["Alert"] = "成功发送加入申请！";
            return RedirectToAction("JoinGroup");
        }

        public ActionResult MemberManage(Guid GroupID)
        {
            Group group = db.Groups.Find(GroupID);
            User user = GetUser();
            var viewModel = new MemberManageViewModel {
                Group = group,
                User = user,
                IsManager = user.StuNum == group.Manager.StuNum ? true : false,
                Requests = new List<Request>()
            };
            foreach(var request in db.Requests.ToList())
            {
                if(request.GroupID==GroupID && request.Agreed==null)
                {
                    viewModel.Requests.Add(request);
                }
            }
            return View(viewModel);
        }

        public ActionResult Accept(Guid RequestID)
        {
            Request request = db.Requests.Find(RequestID);
            User applier = db.Users.Find(request.StuNum);
            Group group = db.Groups.Where(o => o.GroupID == request.GroupID).SingleOrDefault();
            group.Count++;
            group.Users.Add(applier);
            applier.Groups.Add(group);
            request.Agreed = true;
            db.SaveChanges();
            return RedirectToAction("MemberManage",new { GroupID=request.GroupID});
        }

        public ActionResult Dismiss(Guid RequestID)
        {
            Request request = db.Requests.Find(RequestID);
            request.Agreed = false;
            db.SaveChanges();
            return RedirectToAction("MemberManage", new { GroupID = request.GroupID });
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
