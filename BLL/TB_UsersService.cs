using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web.Security;
using System.Web;
using Newtonsoft.Json;
using Common;
using System.IO;
using log4net;
using log4net.Config;
using System.Data.Entity.Validation;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace BLL
{
    public partial class TB_UsersService
    {
        TB_UserRoleService tus = new TB_UserRoleService();
        TB_RoleService trole = new TB_RoleService();
       
    
       
        //校验用户名密码
        public bool ValidateUser(string LoginName, string PassWord)
        {

            IQueryable<TB_Users> user = LoadEntities(s => s.user_name == LoginName && s.user_password == PassWord);
            if (user.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //校验用户名密码
        public bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string LoginName = strTicket.Substring(0, index);
            string PassWord = strTicket.Substring(index + 1);
            string obj = CookieHelper.GetCookieValue(LoginName);
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            TB_Users users = new TB_Users()
            {
                user_name = LoginName,
                user_password = obj
            };
            if (users.user_name == LoginName && users.user_password == PassWord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 远程验证token
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        public Result RemoteTicket(string encryptTicket)
        {
            Result r = new Result()
            {
                Code = "404",
                Msg = "token不能为空!"
            };
          
                if (!string.IsNullOrEmpty(encryptTicket))
                {
                    //解密Ticket
                    var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
                    //从Ticket里面获取用户名和密码
                    var index = strTicket.IndexOf("&");
                    string LoginName = strTicket.Substring(0, index);
                    string PassWord = strTicket.Substring(index + 1);
                    string pwd = CookieHelper.GetCookieValue(LoginName);

                    if (!string.IsNullOrEmpty(pwd))
                    {
                        string fullname = CookieHelper.GetCookieValue(LoginName + "fullname");
                        if (PassWord == pwd)
                        {
                            r.Code = "200";
                            r.Msg = "token正确有效!";
                            r.Data = new { LoginName = LoginName, fullname = fullname };
                        }
                    }
                    else
                    {
                        r.Code = "400";
                        r.Msg = "token无效!";
                    }

                }
           
            return r;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public Result Login(string LoginName, string PassWord)
        {
            Result r = new Result();
          
           
                PassWord = EncryptionMD5.EncryptString(PassWord);
                if (!ValidateUser(LoginName, PassWord))
                {
                    r.Code = "404";
                    r.Msg = "用户名或密码错误";
                    return r;
                }
           
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, LoginName, DateTime.Now,
                               DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", LoginName, PassWord),
                               FormsAuthentication.FormsCookiePath);
                string fullname = LoadEntities(s => s.user_name == LoginName).Select(s => s.fullname).SingleOrDefault();
                //返回登录结果、用户信息、用户验证票据信息
                var Users = new TB_Users { user_name = LoginName, user_password = PassWord, fullname = fullname, Ticket = FormsAuthentication.Encrypt(ticket) };
                //将身份信息保存在session中，验证当前请求是否是有效请求
                // CookieHelper.SetCookie(LoginName, PassWord, DateTime.Now.AddMinutes(20));
                CookieHelper.SetCookie(LoginName, PassWord);
                CookieHelper.SetCookie(LoginName + "fullname", fullname);
                r.Code = "200";
                r.Msg = "登录成功!";
                r.Data = Users;
          
            return r;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Result AddUser(TB_Users users)
        {
            Result result = new Result();
          
                if (LoadEntities(s => s.user_name == users.user_name).Any())
                {
                    result.Code = "400";
                    result.Msg = "该名称已存在!";
                }
                else
                {
                    users.user_password = EncryptionMD5.EncryptString(users.user_password);
                    users.status = "1";
                    users.createtime = DateTime.Now;
                    result.Data = AddEntity(users);
                    result.Code = "200";
                    result.Msg = "添加成功!";
                }
           
            return result;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Result EdtUser(TB_Users users)
        {
            Result result = new Result();
          
                if (users.user_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "要修改的ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.user_id == users.user_id).Any())
                    {
                        users.user_password = EncryptionMD5.EncryptString(users.user_password);
                        users.modifytime = DateTime.Now;
                        UpdateEntity(users);
                        result.Code = "200";
                        result.Msg = "修改成功!";
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该用户不存在!";
                    }

                }
         
            return result;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Result DelUser(TB_Users users)
        {
            Result result = new Result();
          
                if (users.user_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "要删除的ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.user_id == users.user_id).Any())
                    {
                        DeleteEntity(users);
                        result.Code = "200";
                        result.Msg = "删除成功!";
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该用户不存在!";
                    }
                }
           
            return result;
        }
        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public Result GetUserByID(int user_id)
        {
            Result result = new Result();
          
                if (user_id == 0)
                {
                    result.Code = "400";
                    result.Msg = "ID不能为空!";
                }
                else
                {
                    if (LoadEntities(s => s.user_id == user_id).Any())
                    {
                        var user = LoadEntities(s => s.user_id == user_id);
                        var userrole = tus.LoadEntities(l => true);
                        var role = trole.LoadEntities(l => true);
                        var query = userrole.Join(user, a => a.user_id, b => b.user_id, (a, b) => new
                        {
                            a.role_id,
                            user = b
                        }).GroupJoin(role, a => a.role_id, b => b.role_id, (a, b) => new
                        {
                            a.user,
                            rolename = b.Any() ? b.FirstOrDefault().role_name : ""
                        });
                        result.Data = query.FirstOrDefault();
                        result.Code = "200";
                        result.Msg = query.ToString();
                    }
                    else
                    {
                        result.Code = "400";
                        result.Msg = "该用户不存在!";
                    }
                }
          
            return result;
        }
        /// <summary>
        /// 根据条件查询用户
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserName"></param>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Result GetUserByWhere(int Page, int pageSize, string UserName, string DepID)
        {
            Result result = new Result();
          
                int total = 0;
                var query = LoadPageEntities(Page == 0 ? 1 : Page, pageSize == 0 ? 10 : pageSize, out total, s => true, true, o => o.createtime);
                if (!string.IsNullOrEmpty(UserName))
                {
                    query = query.Where(w => w.user_name.Contains(UserName));
                }
                if (!string.IsNullOrEmpty(DepID))
                {
                    query = query.Where(w => w.department_id == Convert.ToInt32(DepID));
                }
                result.Code = "200";
                result.Msg = "查询成功!";
                result.Data = query.ToList();
           
            return result;
        }
        /// <summary>
        /// 上传用户照片
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public Result UploadAvatar(int user_id, HttpFileCollection file)
        {
            Result result = new Result();
          
                // 文件上传后的保存路径
                string filePath = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileName = Path.GetFileName(file[0].FileName);// 原始文件名称
                string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                file[0].SaveAs(filePath + saveName);

                TB_Users user = LoadEntities(s => s.user_id == user_id).FirstOrDefault();
                user.url = "/Uploads/" + saveName;
                _dbSession.Save();

                result.Code = "200";
                result.Msg = "上传成功!";
                result.Data = fileName;
          
            return result;
        }

        public Result P_User(int user_id)
        {
            Result result = new Result();
          
                string pro = "exec p_user @user_id";
                SqlParameter[] param = new SqlParameter[]{
                new SqlParameter{ParameterName="@user_id",Value=user_id}
                };
                DbRawSqlQuery<TB_Users> ret = CurrentRepository.SqlQuery(pro, param);

                result.Code = "200";
                result.Msg = "查询成功!";
                result.Data = ret.ToList();
          
            return result;
        }

        public Result P_EdtUser(int user_id)
        {
            Result result = new Result();
          
                string sql = "update tb_users set remark='1' where user_id=@user_id";
                SqlParameter[] param = new SqlParameter[]{
                new SqlParameter{ParameterName="@user_id",Value=user_id}
                };
                int ret = CurrentRepository.ExcuteSqlCommand(sql, param);

                result.Code = "200";
                result.Msg = "操作成功!";
                result.Data = ret;
         
            return result;
        }


        public Result GetUserRoleName()
        {
            Result result = new Result();
          
                IQueryable<TB_Role> role = trole.LoadEntities(l => true, false, true);
                IQueryable<TB_Users> user = LoadEntities(l => true, false, true);
                //Lambda左连接
                var ret = user.GroupJoin(role, a => a.role_id, b => b.role_id, (a, b) => new
                {
                    user_name = a.user_name,
                    RoleName = b.Any() ? b.FirstOrDefault().role_name : ""
                });
                result.Code = "200";
                result.Msg = "操作成功!";
                result.Data = ret;
          
            return result;
        }
    }
}
