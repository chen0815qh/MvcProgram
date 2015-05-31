using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//这个命名空间就是Required/Compare等数据验证特性所在的命名空间。
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    //之所以使用部分类，是因为每次从数据库更新模型时会覆盖，也就是ConfirmPassword就没了。
    [Serializable]
    public partial class Members
    {
        [Display(Name="确认密码")]
        [Required(ErrorMessage="确认密码不能为空")]
        [Compare("Password",  ErrorMessage = "密码和确认密码必须一致")]
        //当提交表单时，会将该字段的值和Password进行比较，如果不相等，则验证不通过。
        public string ConfirmPassword
        {
            get;
            set;
        }

        //public int MemberId { get; set; }
        //[Display(Name = "会员账号")]
        //[Required(ErrorMessage = "请输入会员账号")]//表示必须要输入。
        //public string MemberAccount { get; set; }
        //[Display(Name = "头像")]

        //public string Portrait { get; set; }
        //[Display(Name = "昵称")]
        //public string NickName { get; set; }
        //[Display(Name = "性别")]
        //public string Gender { get; set; }
        //[Display(Name = "年龄")]
        //[Required(ErrorMessage = "请输入年龄")]
        //[RegularExpression(@"^[1-9](\d{1,2})?$", ErrorMessage = "请输入正确的年龄")]
        //[Range(1, 100, ErrorMessage = "年龄太大或太小")]//该步骤实际上可以在一个正则表达式中完成。
        //public Nullable<int> Age { get; set; }
        //[RegularExpression(@"^[1-9][0-9]{4,}$", ErrorMessage = "请输入正确的QQ号")]
        //public string QQ { get; set; }
        //[RegularExpression(@"^w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)* $", ErrorMessage = "请输入正确的Email")]
        //public string Email { get; set; }
        //public Nullable<System.DateTime> RegisterTime { get; set; }
        //public Nullable<System.DateTime> LastLogin { get; set; }
        //public string LastLoginIP { get; set; }
        //[Required(ErrorMessage = "密码不能为空")]
        //[Display(Name = "密码")]
        //public string Password { get; set; }
    }
}
