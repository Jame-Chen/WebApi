using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [MetadataType(typeof(ClassTabDataAnnotation))]

    //这个必须要和自动生成的类同名,而且必须要在同一命名空间下
    public partial class ClassTab
    {
        //[ForeignKey("ClassId")]
        //public virtual ICollection<Students> Students { get; set; }

    }
    public class ClassTabDataAnnotation
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
       

    }
}
