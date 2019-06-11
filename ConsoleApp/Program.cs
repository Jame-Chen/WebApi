using BLL;
using Common;
using Model;
using Model_Oralce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {

        static void Main(string[] args)
        {

            Test();
            Console.ReadLine();
        }

        public static void Test()
        {
            using (Model2 db = new Model2())
            {
                IQueryable<T_PERSON_NAME> query1 = db.Set<T_PERSON_NAME>().Select(s => s);
                IQueryable<T_PATROL_RECODE> query2 = db.Set<T_PATROL_RECODE>().Where(w => w.N_DEL == 1);
              
                var data = from a in query2
                           join b in query1 on a.S_MAN_ID equals b.S_MAN_ID into b_join
                           from c in b_join.DefaultIfEmpty()
                           select new
                           {
                               a.S_RECODE_ID,
                               a.S_TOWNID,
                               a.S_MAN_ID,
                               ManName = c.S_MAN_NAME
                           };
             
                foreach (var item in  data.ToList())
                {
                    Console.WriteLine(item.S_MAN_ID+"|"+item.ManName);
                }
            }
        }
    }
}
