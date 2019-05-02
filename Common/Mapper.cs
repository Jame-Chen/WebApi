using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Mapper
    {
        public static D MapperTo<D, S>(S s)
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
    }
}
