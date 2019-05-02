using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TB_MenuService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="Menus"></param>
        /// <returns></returns>
        public Result AddMenu(TB_Menu Menus)
        {
            Result result = new Result();


            if (LoadEntities(s => s.menu_name == Menus.menu_name).Any())
            {
                result.Code = "400";
                result.Msg = "该名称已存在!";
            }
            else
            {

                Menus.status = "1";
                result.Data = AddEntity(Menus);
                result.Code = "200";
                result.Msg = "添加成功!";
            }

            return result;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="Menus"></param>
        /// <returns></returns>
        public Result EdtMenu(TB_Menu Menus)
        {
            Result result = new Result();

            if (Menus.menu_id == 0)
            {
                result.Code = "400";
                result.Msg = "要修改的ID不能为空!";
            }
            else
            {
                if (LoadEntities(s => s.menu_id == Menus.menu_id).Any())
                {
                    UpdateEntity(Menus);
                    result.Code = "200";
                    result.Msg = "修改成功!";
                }
                else
                {
                    result.Code = "400";
                    result.Msg = "该菜单不存在!";
                }

            }

            return result;
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Menus"></param>
        /// <returns></returns>
        public Result DelMenu(TB_Menu Menus)
        {
            Result result = new Result();

            if (Menus.menu_id == 0)
            {
                result.Code = "400";
                result.Msg = "要删除的ID不能为空!";
            }
            else
            {
                if (LoadEntities(s => s.menu_id == Menus.menu_id).Any())
                {
                    DeleteEntity(Menus);
                    result.Code = "200";
                    result.Msg = "删除成功!";
                }
                else
                {
                    result.Code = "400";
                    result.Msg = "该菜单不存在!";
                }
            }

            return result;
        }
        /// <summary>
        /// 根据ID获取菜单
        /// </summary>
        /// <param name="menu_id"></param>
        /// <returns></returns>
        public Result GetMenuByID(int menu_id)
        {
            Result result = new Result();

            if (menu_id == 0)
            {
                result.Code = "400";
                result.Msg = "ID不能为空!";
            }
            else
            {
                if (LoadEntities(s => s.menu_id == menu_id).Any())
                {
                    result.Data = LoadEntities(s => s.menu_id == menu_id).FirstOrDefault();
                    result.Code = "200";
                    result.Msg = "查询成功!";
                }
                else
                {
                    result.Code = "400";
                    result.Msg = "该菜单不存在!";
                }
            }

            return result;
        }
        /// <summary>
        /// 根据条件查询菜单
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="MenuName"></param>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Result GetMenuByWhere(int Page, int pageSize, string MenuName)
        {
            Result result = new Result();

            int total = 0;
            var query = LoadPageEntities(Page == 0 ? 1 : Page, pageSize == 0 ? 10 : pageSize, out total, s => true, true, o => o.sort_order);
            if (!string.IsNullOrEmpty(MenuName))
            {
                query = query.Where(w => w.menu_name.Contains(MenuName));
            }

            result.Code = "200";
            result.Msg = "查询成功!";
            result.Data = query.ToList();

            return result;
        }
    }
}
