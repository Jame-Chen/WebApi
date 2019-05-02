using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseRepository<T> where T:class
    {
        T AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        IQueryable<T> LoadEntities( Expression<Func<T, bool>> whereLambda, bool idTracking = false, bool CreationEnabled = true);

        IQueryable<T> LoadPageEntities<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda);

        IQueryable<T> LoadPageEntitiesWithNavigateProperites<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda, Dictionary<string, bool> navigateProperties);
      
         int ExcuteSqlCommand(string strSql, params System.Data.Common.DbParameter[] parameters);

         DbRawSqlQuery<T> SqlQuery(string strSql, params System.Data.Common.DbParameter[] parameters);
   
    }
}
