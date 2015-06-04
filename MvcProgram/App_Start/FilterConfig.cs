using MvcProgram.Attributes;
using System.Web;
using System.Web.Mvc;

namespace MvcProgram
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleExceptionAttribute());//处理程序中未被捕获的异常信息。
            filters.Add(new IdentityCheckAttribute()); //用于进行判断是否登录的过滤器。
        }
    }
}
