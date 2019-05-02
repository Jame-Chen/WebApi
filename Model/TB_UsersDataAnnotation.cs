using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [MetadataType(typeof(TB_UsersDataAnnotation))]

    //这个必须要和自动生成的类同名,而且必须要在同一命名空间下
    public partial class TB_Users
    {
        public string Ticket { get; set; }
    }
    public class TB_UsersDataAnnotation
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "用户ID")]
        [Key]
        public int user_id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [MaxLength(50)]
        [StringLength(50)]
        public string user_name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [MaxLength(50)]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string user_password { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        [Display(Name = "全名")]
        [MaxLength(50)]
        [StringLength(50)]
        public string fullname { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        public int? department_id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [MaxLength(10)]
        [StringLength(10)]
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [DataType(DataType.DateTime)]
        public System.DateTime? createtime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [DataType(DataType.DateTime)]
        public System.DateTime? modifytime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(1000)]
        [StringLength(1000)]
        public string remark { get; set; }
        /// <summary>
        /// 图片url
        /// </summary>
        [Display(Name = "图片url")]
        [MaxLength(200)]
        [StringLength(200)]
        public string url { get; set; }


        public virtual ICollection<TB_UserRole> TB_UserRole { get; set; }
    }
}
