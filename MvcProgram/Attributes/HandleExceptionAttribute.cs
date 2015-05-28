using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProgram.Attributes
{
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            PartialViewResult partial= new PartialViewResult();
            partial.ViewName = "~/Views/Errors/ExceptionMessage.cshtml";
            partial.ViewBag.ExceptionMsg = filterContext.Exception.Message;
            filterContext.Result = partial;
            filterContext.ExceptionHandled = true;//此举代码告诉框架，我们已经手动处理了异常，不需要再去有框架处理异常。
        }
    }
}