using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TB_MenuRoleService
    {
        /// <summary>
        /// 分配菜单
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public Result AssignmentMenu(int role_id, int[] menus)
        {
            Result result = new Result();

            if (role_id != 0 && menus.Count() > 0)
            {
                List<TB_MenuRole> menurolelist = LoadEntities(s => s.role_id == role_id).ToList();
                foreach (TB_MenuRole item in menurolelist)
                {
                    CurrentRepository.DeleteEntity(item);
                }
                foreach (int item in menus)
                {
                    TB_MenuRole tb_menurole = new TB_MenuRole();
                    tb_menurole.role_id = role_id;
                    tb_menurole.menu_id = item;
                    CurrentRepository.AddEntity(tb_menurole);
                }
                _dbSession.Save();
                result.Code = "200";
                result.Msg = "分配成功!";
            }
            else
            {
                result.Code = "400";
                result.Msg = "role_id和菜单不能为空!";
            }

            return result;
        }
    }
}
