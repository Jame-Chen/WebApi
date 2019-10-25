using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using Repository.Interface;
using Repository.Base;
using ModelForSqlServer;
using System.Data;

namespace Repository
{
   public  class BaseRepository<T> :IRepository<T> where T :class
   {
       protected QPContext Context = new QPContext();


       /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return Context.Set<T>().Any(exp);
        }

        /// <summary>
        /// 查找单个，且不被上下文所跟踪
        /// </summary>
        public T FindSingle(Expression<Func<T, bool>> exp)
        {
            return Context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="orderby">排序，格式如："Id"/"Id descending"</param>
        public IQueryable<T> Find<S>(int pageindex, int pagesize,out int Total,Expression<Func<T,S>> orderby, Expression<Func<T, bool>> exp = null,bool isAsc=true)
        {
            if (pageindex < 1) pageindex = 1;
            IQueryable<T> query = Filter(exp);
            Total = query.Count();
            if (isAsc)
            {
                query = query.OrderBy(orderby);
            }
            else {
                query = query.OrderByDescending(orderby);
            }
            return query.Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }

        public void Add(T entity)
        {
            //if (string.IsNullOrEmpty(entity.Id))
            //{
            //    entity.Id = Guid.NewGuid().ToString();
            //}
            Context.Set<T>().Add(entity);
            Save();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd(T[] entities)
        {
            //foreach (var entity in entities)
            //{
            //    entity.Id = Guid.NewGuid().ToString();
            //}
            Context.Set<T>().AddRange(entities);
            Save();
        }

        public void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
            foreach (System.Reflection.PropertyInfo p in entity.GetType().GetProperties())
            {
                string type = p.PropertyType.Name.ToString();
                if (p.Name == type)
                {
                    continue;
                }
                if (p.GetValue(entity) == null)
                {
                    if (Context.Entry(entity).Property(p.Name).IsModified)
                    {
                        Context.Entry(entity).Property(p.Name).IsModified = false;
                    }
                }
            }
            //如果数据没有发生变化
            if (!this.Context.ChangeTracker.HasChanges())
            {
                return;
            }

            Save();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Save();
        }

        /// <summary>
        /// 按指定id更新实体,会更新整个实体
        /// </summary>
        /// <param name="identityExp">The identity exp.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, object>> identityExp, T entity)
        {
            Context.Set<T>().AddOrUpdate(identityExp, entity);
            Save();
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            Context.Set<T>().Where(where).Update(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> exp)
        {
            Context.Set<T>().Where(exp).Delete();
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            }
        }

        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
        {
            var dbSet = Context.Set<T>().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

       public int ExecuteSql(string sql)
       {
          return  Context.Database.ExecuteSqlCommand(sql);
       }
   }
}
