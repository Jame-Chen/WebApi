using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        public string Name { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>

        [Display(Name = "姓名")]

        [MaxLength(200, ErrorMessage = "最多只能输入200个字符")]

        [StringLength(200, ErrorMessage = "最多只能输入200个字符")]


        public string MyName { get; set; }


        /// <summary>
        /// 密码
        /// </summary>

        [Display(Name = "密码")]

        [MaxLength(200, ErrorMessage = "最多只能输入200个字符")]

        [StringLength(200, ErrorMessage = "最多只能输入200个字符")]

        [DataType(DataType.Password)]


        public string Password { get; set; }


        /// <summary>
        /// 确认密码
        /// </summary>

        [Display(Name = "确认密码")]

        [MaxLength(200, ErrorMessage = "最多只能输入200个字符")]

        [StringLength(200, ErrorMessage = "最多只能输入200个字符")]

        [DataType(DataType.Password)]


        public string SurePassword { get; set; }

         public string Token { get; set; }
    }
}
