using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;
using BLL;
using WebAPI.Filter;

namespace WebAPI.Controllers
{
    
    
    public class RoleController : ApiController
    {
        TB_RoleService ts = new TB_RoleService();
        TB_MenuRoleService tms = new TB_MenuRoleService();
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddRole(TB_Role Roles)
        {

            return ts.AddRole(Roles);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EdtRole(TB_Role Roles)
        {
            return ts.EdtRole(Roles);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public Result DelRole(TB_Role Roles)
        {
            return ts.DelRole(Roles);
        }
        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="Role_id"></param>
        /// <returns></returns>
        public Result GetRoleByID(int Role_id)
        {
            return ts.GetRoleByID(Role_id);
        }
        /// <summary>
        /// 根据条件查询角色
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="RoleName"></param>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Result GetRoleByWhere(int Page, int pageSize, string RoleName)
        {
            return ts.GetRoleByWhere(Page, pageSize, RoleName);
        }
        /// <summary>
        /// 分配菜单
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public Result AssignmentMenu(int role_id, int[] menus)
        {
            return tms.AssignmentMenu(role_id, menus);
        }
    }
}