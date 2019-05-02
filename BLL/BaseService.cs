using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DAL;
using Model;

namespace BLL
{
    public abstract class BaseService<T> where T : class,new()
    {
        //创建实体层的访问实例
        protected IBaseRepository<T> CurrentRepository { get; set; }

        public IDbSession _dbSession = DbSessionFactory.GetCurrentDbSession();

        public abstract void SetCurrentRepository();

        public BaseService()
        {
            SetCurrentRepository();
        }


        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result AddEntity(T entity)
        {
            Result result = new Result();
            var addEntity = CurrentRepository.AddEntity(entity);
            _dbSession.Save();
            result.Data = addEntity;
            return result;
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result UpdateEntity(T entity)
        {
            Result result = new Result();
            CurrentRepository.UpdateEntity(entity);
            _dbSession.Save();
            return result;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result DeleteEntity(T entity)
        {
            Result result = new Result();
            CurrentRepository.DeleteEntity(entity);
            _dbSession.Save();
            return result;
        }

        /// <summary>
        /// 简单查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, bool idTracking = false, bool CreationEnabled = true)
        {
            return CurrentRepository.LoadEntities(whereLambda, idTracking, CreationEnabled);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda)
        {
            return CurrentRepository.LoadPageEntities<S>(pageNum, pageSize, out total, whereLambda, isAsc, orderByLambda);
        }

        public IQueryable<T> LoadPageEntitiesWithNavigateProperites<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda, Dictionary<string, bool> navigateProperties)
        {
            return CurrentRepository.LoadPageEntitiesWithNavigateProperites<S>(pageNum, pageSize, out total, whereLambda, isAsc, orderByLambda, navigateProperties);
        }
    }
}

