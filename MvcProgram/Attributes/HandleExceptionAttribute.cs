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
            filterContext.ExceptionHandled = true;
        }
    }
}