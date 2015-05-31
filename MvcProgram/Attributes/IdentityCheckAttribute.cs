using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcProgram.Attributes
{
    public class IdentityCheckAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 除了带Skip标签的Action方法之外，没有带IdentityCheck标签的控制器或者Action方法也不需要登录。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionDescriptor actionDescriptor = filterContext.ActionDescriptor;//表示对当前请求的Action方法的一些描述信息
            ControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;//表示对当前请求的控制器类的一些描述信息。
            //注意：MVC中，浏览器地址栏中输入的任何地址最终都会通过被匹配的路由中找出Controller（控制器)和 Action方法，然后进行调用。
            //IsDefind()表示某个Action或者Controller是否被贴上了某个特性(继承自Attribute)的类。
            if ((controllerDescriptor.IsDefined(typeof(IdentityCheckAttribute), true) && !actionDescriptor.IsDefined(typeof(SkipAttribute), true))
            ||
                actionDescriptor.IsDefined(typeof(IdentityCheckAttribute), true)
                )
            {
                //这里需要注意的是：如果你想取到像WebForm中的Session.你可以使用System.Web.HttpContext.Current.Session
                //需要登录,但是有没有登录。
                if (filterContext.HttpContext.Session["MemberInfo"] == null)
                {
                    //表示当前请求是否来自于异步请求。
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //如果是异步请求，返回一个Json对象给浏览器，浏览器的异步请求代码中判断要判断返回的是否是nologin状态，如果是的话
                        //记得重定向。
                        var obj = new { Status = "nologin", Msg = "您还没有登录，请先登录", NextUrl = "/Members/Login?ReturnUrl=" + HttpContext.Current.Request.RawUrl };
                        filterContext.Result = new JsonResult() { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else
                    {
                        //跳转到登录页。
                        filterContext.Result = new RedirectToRouteResult("Default",new RouteValueDictionary() { 
                        {"controller","Members"},
                        {"action","Login"},
                        {
                            "ReturnUrl",HttpContext.Current.Request.RawUrl
                        }   
                        });         //new ViewResult() { ViewName = "~/Views/Members/Login.cshtml" };
                        //filterContext.HttpContext.Response.Write("<script type='text/javascript'>alert('您还没有登录，请先登录!');</script>");
                    }

                }
            }
            //base.OnActionExecuting(filterContext);
        }
    }
}