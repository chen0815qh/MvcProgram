using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//这个命名空间就是Required/Compare等数据验证特性所在的命名空间。
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    //之所以使用部分类，是因为每次从数据库更新模型时会覆盖
    public partial class Members
    {
        [Display(Name="确认密码")]
        [Required]
        [Compare("Password", ErrorMessage = "密码和确认密码必须一致")]
        //当提交表单时，会将该字段的值和Password进行比较，如果不相等，则验证不通过。
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
