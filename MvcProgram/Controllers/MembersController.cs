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
        [ValidateInput(false)]
        [HttpPost]
        //注意：url查询字符串中的参数值或者表单提交的值会自动映射到当前这个action的参数，但是名称必须保持一致。
        public ActionResult Reply(int msgId,int memberId,string content)
        {
            Members member = Session["MemberInfo"] as Members;
            //当在留言板中使用回复功能时，当前登录的用户就是回复者，而这条消息的
            //接受者就是原来给当前会员留言者。
            LeaveMessage msg = new LeaveMessage() { 
            MsgContent=content,
            MemberId=memberId,
            AuthorId=member.MemberId,
            AddTime=DateTime.Now,
            IP=Request.UserHostAddress,
            ReplyId=msgId
            };
            context.LeaveMessage.Add(msg);
            context.SaveChanges();
            var obj = new { Member = member, Msg = msg };
            return Json(obj);//如果是get请求，那么请记得指定第二个参数return Json(obj,JsonRequestBehavior.AlowGet);否则报错
        }

        public ActionResult Leave(int id,int pageIndex=1)
        {
            //这里读取出目标会员信息(我们要给其留言的会员的信息)，用于在前台显示 给 某某 留言时的信息提取。
            Members member = context.Members.Where(m => m.MemberId == id).FirstOrDefault();
            Session["Member"] = member;
            if (member == null)
            {
                //会员信息不存在
                return RedirectToRoute("MemberList");
            }

            Members currentMember = Session["MemberInfo"] as Members;//登录系统的会员，该会员要给上述的会员留言。

            ViewBag.PageSize = 10;
            ViewBag.PageIndex = pageIndex;
            // context.LeaveMessage.Where(m => m.MemberId == member.MemberId || (m.AuthorId == member.MemberId && m.ReplyId != 0 && member.MemberId == id)).Count();
            //读取出我们给别人留言的总数。
            ViewBag.LeaveCount = context.LeaveMessage.Where(m => m.AuthorId == currentMember.MemberId && m.ReplyId == 0).Count();
            //读取出给别人的留言，以及回复(自己给会员的回复和会员给我们的回复)等。
            List<ReplyList> replyList = context.Proc_GetReplyList(pageIndex, 10, currentMember.MemberId, id).ToList();
            ViewBag.ReplyList = replyList;
            return View(member);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]//关闭XSS攻击检查,注意：设置了此值之后还要设置网站应用根目录下的web.config <httpRuntime requestValidationMode="2.0"/>才行
        public ActionResult Leave(LeaveMessage message)
        {
            if (string.IsNullOrEmpty(Request.Form["editorValue"]))
            {
                ViewData["MsgTip"] = "请输入留言内容";
                return View();
            }
            Members member = Session["Member"] as Members;
            int id = 0;
            if (member == null)
            {
                id = Convert.ToInt32(RouteData.Values["id"]);
                member = context.Members.Where(m => m.MemberId == id).FirstOrDefault();
            }
            if (member == null)
            {
                return RedirectToRoute("MemberList");
            }
            id = member.MemberId;
            Members author = Session["MemberInfo"] as Members;
            message.MemberId = id;
            message.AuthorId = author.MemberId;
            message.IP = Request.UserHostAddress;
            //editorValue：用于UEditor提交的表单元素的值 Request.Form["editorValue"]
            message.MsgContent = HttpUtility.HtmlEncode(Request.Form["editorValue"]);
            message.ReplyId = 0;
            message.AddTime = DateTime.Now;
            context.LeaveMessage.Add(message);
            context.SaveChanges();
            ViewData["MsgTip"] = "留言成功";
            return View(member);
        }
        //
        // GET: /Members/
        public ActionResult Index(int pageIndex)//必须和路由中(RouteConfig.cs中注册的)指定的参数(在{}中的名称，不包括{controller}和{action})的名称保持一致。
        {
            int pageSize = 12;
            ViewBag.PageSize = pageSize;
            //这里读取出所有的会员信息。
            //ViewBag.Members = context.Members.ToList();
            //分页获得会员数据。
            ViewBag.MemberCount = context.Members.Count();
            List<Members> memberList = context.Members.OrderByDescending(m => m.RegisterTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return View(memberList);
        }

        public ActionResult MessageBoard(int id = 1)
        {
            Members member = Session["MemberInfo"] as Members;
            ViewBag.PageSize = 10;
            //留言总数。
            ViewBag.MessageCount = context.LeaveMessage.Where(msg => msg.ReplyId == 0 && msg.MemberId == member.MemberId).Count();
            //这里获得某个会员的留言板的所有的记录，每页显示10条,这里包括了留言和回复哦。
            List<MsgList> msgList = context.Proc_GetMessageList(id, 10, member.MemberId).ToList();//不要忘了引用System.Data.Entity这个程序集
            
            return View(msgList);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            //ModelState.AddModelError("1", "测试一下"); //当前端使用Html.ValidationSummary(false)的时候，所有的错误信息将显示。
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(FormCollection form)
        {

            string account = form["MemberAccount"];
            string password = form["Password"];

            if (string.IsNullOrEmpty(account))
            {
                //"",表示模型级错误，就是不针对于某个属性进行的错误提示。
                ModelState.AddModelError("", "请输入用户名");
            }
            else if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "请输入密码");
            }
            else
            {
                Members member = context.Members.Where(m => m.MemberAccount == account && m.Password == password).SingleOrDefault();
                if (member != null)
                {
                    //登录成功
                    Session["MemberInfo"] = member;
                    return RedirectToAction("Index", "Members", new { pageIndex = 1 });//这里跳转到交友广场。
                }
                else
                {
                    //用户名或者密码错误.对于传给AddModelError的key参数的值为"",可以通过Html.ValidationSummary来显示。
                    ModelState.AddModelError("", "用户名或密码错误");
                }
            }

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
                Request.Files[0].SaveAs(Server.MapPath("~/Content/Images/" + filePath));
                ViewData["Msg"] = "注册成功";
            }
            return View();
        }
    }
}