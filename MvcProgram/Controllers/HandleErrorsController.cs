using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProgram.Controllers
{
    public class HandleErrorsController : Controller
    {
        //
        // GET: /HandleErrors/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 当发生404错误的时候跳转到哪里。注意：这个在web.config文件中进行了配置。
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            return View();
        }
	}
}