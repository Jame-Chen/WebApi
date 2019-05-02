using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
     [MetadataType(typeof(StudentDataAnnotation))]
    //这个必须要和自动生成的类同名,而且必须要在同一命名空间下
    public partial class Student
    {
         public string Sexs { get; set; }
    }
    public  class StudentDataAnnotation
    {
        public int Id { get; set; }
        [Display(Name="年龄")]
        public Nullable<int> Age { get; set; }
         [Display(Name = "学生名称")]
        public string Name { get; set; }
         [Display(Name = "性别")]
        public Nullable<int> Sex { get; set; }
         [Display(Name = "语文")]
        public Nullable<decimal> Chinese { get; set; }
         [Display(Name = "数学")]
        public Nullable<decimal> Math { get; set; }
         [Display(Name = "英语")]
        public Nullable<decimal> English { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "英语")]
        public virtual ICollection<Attributes> Attributes { get; set; }
    }
}
