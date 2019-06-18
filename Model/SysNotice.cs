
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace Model
{

using System;
    using System.Collections.Generic;
    
using System.ComponentModel.DataAnnotations;
/// <summary>
/// SysNotice
/// </summary>
[Serializable]
public partial class SysNotice
{
public SysNotice()
    {

	}
			
    
/// <summary>
    /// 主键
    /// </summary>

    [Display(Name = "主键")]

    [MaxLength(36,ErrorMessage="最多只能输入36个字符")]

    [StringLength(36,ErrorMessage="最多只能输入36个字符")]

    [Key]


    public string Id { get; set; }

    
/// <summary>
    /// 内容
    /// </summary>

    [Display(Name = "内容")]

    [MaxLength(4000,ErrorMessage="最多只能输入4000个字符")]

    [StringLength(4000,ErrorMessage="最多只能输入4000个字符")]


    public string Message { get; set; }

    
/// <summary>
    /// 失效时间
    /// </summary>

    [Display(Name = "失效时间")]

    [DataType(DataType.DateTime)]


    public System.DateTime? LostTime { get; set; }

    
/// <summary>
    /// 人员
    /// </summary>

    [Display(Name = "人员")]

    [MaxLength(36,ErrorMessage="最多只能输入36个字符")]

    [StringLength(36,ErrorMessage="最多只能输入36个字符")]


    public string PersonId { get; set; }

    
/// <summary>
    /// 备注
    /// </summary>

    [Display(Name = "备注")]

    [MaxLength(4000,ErrorMessage="最多只能输入4000个字符")]

    [StringLength(4000,ErrorMessage="最多只能输入4000个字符")]


    public string Remark { get; set; }

    
/// <summary>
    /// 状态
    /// </summary>

    [Display(Name = "状态")]

    [MaxLength(200,ErrorMessage="最多只能输入200个字符")]

    [StringLength(200,ErrorMessage="最多只能输入200个字符")]


    public string State { get; set; }

    
/// <summary>
    /// 创建时间
    /// </summary>

    [Display(Name = "创建时间")]

    [DataType(DataType.DateTime)]


    public System.DateTime? CreateTime { get; set; }

    
/// <summary>
    /// 创建人
    /// </summary>

    [Display(Name = "创建人")]

    [MaxLength(200,ErrorMessage="最多只能输入200个字符")]

    [StringLength(200,ErrorMessage="最多只能输入200个字符")]


    public string CreatePerson { get; set; }

}

}
