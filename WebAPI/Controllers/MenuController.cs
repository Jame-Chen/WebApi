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
    
    
    public class MenuController : ApiController
    {
        TB_MenuService ts = new TB_MenuService();
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddMenu(TB_Menu Menus)
        {

            return ts.AddMenu(Menus);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="Menus"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EdtMenu(TB_Menu Menus)
        {
            return ts.EdtMenu(Menus);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Menus"></param>
        /// <returns></returns>
        public Result DelMenu(TB_Menu Menus)
        {
            return ts.DelMenu(Menus);
        }
        /// <summary>
        /// 根据ID获取菜单
        /// </summary>
        /// <param name="Menu_id"></param>
        /// <returns></returns>
        public Result GetMenuByID(int Menu_id)
        {
            return ts.GetMenuByID(Menu_id);
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
            return ts.GetMenuByWhere(Page, pageSize, MenuName);
        }
    }
}
