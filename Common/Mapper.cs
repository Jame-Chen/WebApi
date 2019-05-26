using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Mapper
    {
        public static D MapperTo<D>(object s)
        {
            D d = Activator.CreateInstance<D>();
            var sType = s.GetType();
            var dType = typeof(D);
            foreach (PropertyInfo sP in sType.GetProperties())
            {
                foreach (PropertyInfo dP in dType.GetProperties())
                {
                    if (dP.Name == sP.Name)
                    {
                        dP.SetValue(d, sP.GetValue(s));
                        break;
                    }
                }
            }
            return d;
        }

        public static List<D> MapperToList<D>(object s)
        {
            List<D> ListD = new List<D>();
            foreach (var item in s as IEnumerable<object>)
            {
                D d = Activator.CreateInstance<D>();

                var sType = item.GetType();

                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(item));
                            break;
                        }
                    }
                }
                ListD.Add(d);
            }
            return ListD;
        }
    }
}
