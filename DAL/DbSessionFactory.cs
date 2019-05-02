using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbSession()
        {
            IDbSession _dbSession = CallContext.GetData("DbSession") as IDbSession;
            if (_dbSession == null)
            {
                _dbSession = new DbSession();
                CallContext.SetData("DbSession", _dbSession);
            }
            return _dbSession;
        }
    }
}
