using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TB_UserRoleService
    {
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public Result AssigningRoles(int user_id, int[] roles)
        {
            Result result = new Result();
          
                if (user_id != 0 && roles.Count() > 0)
                {
                    List<TB_UserRole> userrolelist = LoadEntities(s => s.user_id == user_id).ToList();
                    foreach (TB_UserRole item in userrolelist)
                    {
                        CurrentRepository.DeleteEntity(item);
                    }
                    foreach (int item in roles)
                    {
                        TB_UserRole tb_userrole = new TB_UserRole();
                        tb_userrole.role_id = item;
                        tb_userrole.user_id = user_id;
                        CurrentRepository.AddEntity(tb_userrole);
                    }
                    _dbSession.Save();
                    result.Code = "200";
                    result.Msg = "分配成功!";
                }
                else
                {
                    result.Code = "400";
                    result.Msg = "userid和角色不能为空!";
                }
         
            return result;
        }
    }
}
