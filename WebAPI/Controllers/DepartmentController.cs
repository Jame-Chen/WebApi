using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Filter;

namespace WebAPI.Controllers
{
    
    
    public class DepartmentController : ApiController
    {
        TB_DepartmentService db = new TB_DepartmentService();
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddDepartment(TB_Department Departments)
        {

            return db.AddDepartment(Departments);
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="Departments"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EdtDepartment(TB_Department Departments)
        {
            return db.EdtDepartment(Departments);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Departments"></param>
        /// <returns></returns>
        public Result DelDepartment(TB_Department Departments)
        {
            return db.DelDepartment(Departments);
        }
        /// <summary>
        /// 根据ID获取部门
        /// </summary>
        /// <param name="Department_id"></param>
        /// <returns></returns>
        public Result GetDepartmentByID(int Department_id)
        {
            return db.GetDepartmentByID(Department_id);
        }
        /// <summary>
        /// 根据条件查询部门
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Result GetDepartmentByWhere(int Page, int pageSize, string DepartmentName)
        {
            return db.GetDepartmentByWhere(Page, pageSize, DepartmentName);
        }
    }
}
