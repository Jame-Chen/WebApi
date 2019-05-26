using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class ExpManagementDetail
   {
       public string S_Mange_Id { get; set; }
       public string S_TOWNID { get; set; }
       public string Category { get; set; }
       public string Type { get; set; }
       public string Location { get; set; }
       public string Desc { get; set; }
       public string Status { get; set; }
       public string Time { get; set; }
       public int CLtime { get; set; }
       public string N_x { get; set; }
       public string N_y { get; set; }
       public string MangeMan { get; set; }
       public string MangeCompany { get; set; }
       public string W_Taskno { get; set; }
       public string MapUrl { get; set; }
       public List<string> UrlList { get; set; }
       public List<string> UrlListCZ { get; set; }
    }
}
