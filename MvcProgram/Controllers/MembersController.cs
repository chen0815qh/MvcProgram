using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProgram.Controllers
{
    public class MembersController : Controller
    {
        MessageBoardSysEntities context = new MessageBoardSysEntities();
        //
        // GET: /Members/
        public ActionResult Index()
        {
            //这里读取出所有的会员信息。
            //ViewBag.Members = context.Members.ToList();
            return View(context.Members.ToList());
        }

        public ActionResult MessageBoard()
        {
            return View();
        }



        [AcceptVerbs("GET")]
        //表示当前Register方法只允许GET请求，
        //如果是[AcceptVerbs("GET","POST"]那么表示既可以响应GET类型的请求，也可以响应POST请求。
        public ActionResult Register()
        {
            return View();
        }

        //指定该Action方法只有在POST的请求的情况下才可以访问。
        [HttpPost]
        public ActionResult Register(Members member)
        {
            if (Request.Files.Count <= 0)
            {
                ModelState.AddModelError("Portrait", "请上传头像");
            }
            else if (!Request.Files[0].ContentType.StartsWith("image/"))
            {
                //这里添加错误提示信息。这里添加的信息，会在.cshtml文件中使用Html.ValidationMessage("Portrait")或者Html.ValidationMessageFor(m=>m.Portrait)
                //的地方显示。
                ModelState.AddModelError("Portrait", "只能上传图片文件");
            }
            if (ModelState.IsValid)
            {
                //这两进行注册。
                MessageBoardSysEntities entities = new MessageBoardSysEntities();
                string filePath = Guid.NewGuid() + Request.Files[0].FileName;
                member.Portrait = filePath;
                entities.Members.Add(member);
                entities.SaveChanges();
                Request.Files[0].SaveAs(Server.MapPath("~/Content/Images/" +filePath));
            }
            return View();
        }
    }
}