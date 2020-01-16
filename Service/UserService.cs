using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace Service
{
    public class UserService : BaseService<User>
    {
        public Result Add(User user)
        {
            Result ret = new Result();
            Repository.Add(user);
            return ret;
        }

        public Result Test()
        {
            Result ret = new Result();
            Expression<Func<User, bool>> exp = w => true;
            exp = exp.And(w => w.Name.Contains("T"));
            var userq = UnitWork.Find<User>(exp);
            return ret;
        }
    }
}
