using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Model;


namespace IBLL
{
    public interface IBaseService<T> where T : class,new()
    {
        //创建实体层的访问实例
        //protected IBaseRepository<T> CurrentRepository{get;set;}

        void SetCurrentRepository();

        Result AddEntity(T entity);

        Result UpdateEntity(T entity);

        Result DeleteEntity(T entity);

        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, bool idTracking = false, bool CreationEnabled = true);

        IQueryable<T> LoadPageEntities<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda);

        IQueryable<T> LoadPageEntitiesWithNavigateProperites<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda, Dictionary<string, bool> navigateProperties);
    }
}
