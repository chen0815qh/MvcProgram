﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//这个命名空间就是Required/Compare等数据验证特性所在的命名空间。
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    //之所以使用部分类，是因为每次从数据库更新模型时会覆盖
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
        //public string MemberAccount { get; set; }
        //public string Portrait { get; set; }
        //public string NickName { get; set; }
        //public string Gender { get; set; }
        //public Nullable<int> Age { get; set; }
        //public string QQ { get; set; }
        //public string Email { get; set; }
        //public Nullable<System.DateTime> RegisterTime { get; set; }
        //public Nullable<System.DateTime> LastLogin { get; set; }
        //public string LastLoginIP { get; set; }
        //public string Password { get; set; }
    }
}
