using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Model;
using IDAL;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace DAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository()
        {
            //取消EF的懒加载模式
            //  db.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// 声明上下文
        /// </summary>
        private DbContext db = EFContextFactory.GetCurrentDbContext();

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {

            //EF 4.0
            //db.CreateObectSet<T>().AddObject(entity);

            //EF 5.0
            db.Entry<T>(entity).State = EntityState.Added;
            return entity;
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            foreach (System.Reflection.PropertyInfo p in entity.GetType().GetProperties())
            {
                string type = p.PropertyType.Name.ToString();
                if (p.Name == type)
                {
                    continue;
                }
                if (p.GetValue(entity) == null)
                {
                    if (db.Entry(entity).Property(p.Name).IsModified)
                    {
                        db.Entry(entity).Property(p.Name).IsModified = false;
                    }
                }
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// 简单查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, bool idTracking = false, bool CreationEnabled = true)
        {
            if (CreationEnabled)
            {
                db.Configuration.ProxyCreationEnabled = false;
            }
            IQueryable<T> query = null;
            if (idTracking)
            {
                query = db.Set<T>().Where(whereLambda).AsQueryable();
            }
            else
            {
                query = db.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();
            }
            return query;
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
            var temp = db.Set<T>().AsNoTracking().Where(whereLambda);
            total = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy(orderByLambda)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending(orderByLambda)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize);
            }
            return temp.AsQueryable();
        }

        /// <summary>
        /// 数据较大时使用该方法
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="navigateProperties">要附加的导航属性名称与所附加的属性是否是集合</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntitiesWithNavigateProperites<S>(int pageNum, int pageSize, out int total, Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderByLambda, Dictionary<string, bool> navigateProperties)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var temp = db.Set<T>().Where(whereLambda);
            total = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy(orderByLambda)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending(orderByLambda)
                        .Skip(pageSize * (pageNum - 1))
                        .Take(pageSize);
            }
            foreach (var item in temp)
            {
                foreach (var p in navigateProperties)
                {
                    if (p.Value)
                    {
                        db.Entry<T>(item).Collection(p.Key).Load();
                    }
                    else
                    {
                        db.Entry<T>(item).Reference(p.Key).Load();
                    }
                }
            }
            return temp.AsQueryable();
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExcuteSqlCommand(string strSql, params System.Data.Common.DbParameter[] parameters)
        {
            return db.Database.ExecuteSqlCommand(strSql, parameters);
        }
        /// <summary>
        /// sql查询语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbRawSqlQuery<T> SqlQuery(string strSql, params System.Data.Common.DbParameter[] parameters)
        {
            return db.Database.SqlQuery<T>(strSql, parameters);
        }
    }
}
