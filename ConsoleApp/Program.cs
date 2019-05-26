using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        static SysPersonService sps = new SysPersonService();

        static void Main(string[] args)
        {

            Test();
            Console.ReadLine();
        }

        public static void Test()
        {
            IQueryable<SysPerson> list = sps.LoadEntities(l => true);
            List<Person> plist = Mapper.MapperToList<Person>(list);

            foreach (Person item in plist)
            {
                Console.WriteLine(item.MyName + " " + item.Name + " " + item.Password);
            }
        }
    }
}
