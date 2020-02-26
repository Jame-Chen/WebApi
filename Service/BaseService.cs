using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Interface;
using Repository.Base;
using Model;

namespace Service
{
    public class BaseService<T> where T : class
    {
        /// <summary>
        /// 用于事务操作
        /// </summary>
        /// <value>The unit work.</value>
        public IUnitWork UnitWork { get; set; }
        /// <summary>
        /// 用于普通的数据库操作
        /// </summary>
        /// <value>The repository.</value>
        public IRepository<T> Repository { get; set; }

        public List<T> Get()
        {
            return Repository.Find(f => true).ToList();
        }

        public Result Add(T entity)
        {
            Result ret = new Result();
            try
            {
                Repository.Add(entity);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }

        public Result Update(T entity)
        {
            Result ret = new Result();
            try
            {
                Repository.Update(entity);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }
        public Result Delete(T entity)
        {
            Result ret = new Result();
            try
            {
                Repository.Delete(entity);
            }
            catch (Exception e)
            {
                ret.Code = "500";
                ret.Msg = e.Message;
                throw;
            }
            return ret;
        }
    }
}
