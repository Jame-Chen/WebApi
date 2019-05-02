using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TB_RoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public Result AddRole(TB_Role Roles)
        {
            Result result = new Result();
          
           
                if (LoadEntities(s => s.role_name == Roles.role_name).Any())
                {
                    result.Code = "400";
                    result.Msg = "该名称已存在!";
                }
                else
                {

                    Roles.createtime = DateTime.Now;
                    result.Data = AddEntity(Roles);
                    result.Code = "200";
                    result.Msg = "添加成功!";
                }
          
            return result;
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public Result EdtRole(TB_Role Roles)
        {
            Result result = new Result();
          
         
                if (Roles.role_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "要修改的ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.role_id == Roles.role_id).Any())
                    {

                        Roles.modifytime = DateTime.Now;
                        UpdateEntity(Roles);
                        result.Code = "200";
                        result.Msg = "修改成功!";
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该角色不存在!";
                    }

                }
         
            return result;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public Result DelRole(TB_Role Roles)
        {
            Result result = new Result();
          
          
                if (Roles.role_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "要删除的ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.role_id == Roles.role_id).Any())
                    {
                        DeleteEntity(Roles);
                        result.Code = "200";
                        result.Msg = "删除成功!";
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该角色不存在!";
                    }
                }
         
            return result;
        }
        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public Result GetRoleByID(int role_id)
        {
            Result result = new Result();
          
           
                if (role_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.role_id == role_id).Any())
                    {
                        result.Data = LoadEntities(s => s.role_id == role_id).FirstOrDefault();
                        result.Code = "200";
                        result.Msg = "查询成功!";
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该角色不存在!";
                    }
                }
         
            return result;
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
            Result result = new Result();
          
           
                int total = 0;
                var query = LoadPageEntities(Page == 0 ? 1 : Page, pageSize == 1 ? 10 : pageSize, out total, s => true, true, o => o.createtime);
                if (!string.IsNullOrEmpty(RoleName))
                {
                    query = query.Where(w => w.role_name.Contains(RoleName));
                }

                result.Code = "200";
                result.Msg = "查询成功!";
                result.Data = query.ToList();
          
            return result;
        }
    }
}
