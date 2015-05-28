using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcProgram
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:"MemberList",
                url:"memberlist/{page}/{pageIndex}",//url中不一定要出现{controller}和{action},那么此时，每次访问members/page/1或者members类似的url时，
                //其实都是去访问Members这个controller下的Index这个方法。
                defaults: new { controller="Members",action="Index",page="page",pageIndex=1},
                constraints: new { pageIndex=@"^[1-9]\d*$"}//表示URL中的pageIndex占位符部分的值必须是一个>0的数才会去匹配该路由。
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
