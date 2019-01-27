using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace co_sport.Attributes
{
    public class CheckLoginAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["User"]==null)
            {
                httpContext.Response.StatusCode = 401;
                return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            if(filterContext.HttpContext.Response.StatusCode==401)
            {
                filterContext.Result = new RedirectResult(string.Format("~/User/Login?returnUrl={0}", filterContext.HttpContext.Request.RawUrl));
                //filterContext.HttpContext.Response.Redirect("~/User/Login");
            }
        }


        /*参考
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool pass = false;
            if (httpContext.Session["User"] == null)
            {
                httpContext.Response.StatusCode = 401;
            }
            else
            {
                pass = true;
            }
            return pass;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult(string.Format("~/Account/Login?returnUrl={0}", filterContext.HttpContext.Request.RawUrl));
            }
        }*/
    }
}