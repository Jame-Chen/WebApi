using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [MetadataType(typeof(StudentsDataAnnotation))]

    //这个必须要和自动生成的类同名,而且必须要在同一命名空间下
    public partial class Students
    {
        //[ForeignKey("ClassId")]
        //public virtual ClassTab ClassTab { get; set; }
    }
    public  class StudentsDataAnnotation
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Sex { get; set; }
        public Nullable<decimal> Chinese { get; set; }
        public Nullable<decimal> Math { get; set; }
        public Nullable<decimal> English { get; set; }
        public string PictruePath { get; set; }
        public int ClassId { get; set; }
     

    }
}
