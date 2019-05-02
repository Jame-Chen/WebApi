using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Web.Security;
using System.Web;
using Common;
using WebAPI.Filter;
using System.Text;
using System.Data.Entity.Validation;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace WebAPI.Controllers
{
    
    
    public class UsersController : ApiController
    {
        TB_UsersService us = new TB_UsersService();
        TB_UserRoleService tus = new TB_UserRoleService();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Result Login(string LoginName, string PassWord)
        {
            return us.Login(LoginName, PassWord);
        }
        /// <summary>
        /// 远程验证token
        /// </summary>
        /// <param name="encryptTicket"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Result RemoteTicket(string encryptTicket)
        {
            return us.RemoteTicket(encryptTicket);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        [HttpPost]
        public Result AddUser(TB_Users users)
        {

            return us.AddUser(users);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public Result EdtUser(TB_Users users)
        {
            return us.EdtUser(users);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Result DelUser(TB_Users users)
        {
            return us.DelUser(users);
        }
        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public Result GetUserByID(int user_id)
        {
            return us.GetUserByID(user_id);
        }
        /// <summary>
        /// 根据条件查询用户
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserName"></param>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Result GetUserByWhere(int Page = 1, int pageSize = 10, string UserName = null, string DepID = null)
        {
            return us.GetUserByWhere(Page, pageSize, UserName, DepID);
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="roles">int[]数组</param>
        /// <returns></returns>
        public Result AssigningRoles(int user_id, int[] roles)
        {
            return tus.AssigningRoles(user_id, roles);
        }
        /// <summary>
        /// 上传用户照片
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public Result UploadAvatar([SwaggerFileUpload]int user_id)
        {
            //Result result = new Result();
            //try
            //{
            //    if (!Request.Content.IsMimeMultipartContent())
            //    {
            //        result.Code = "500";
            //        result.Msg = "不识别的媒体类型!";
            //        return result;
            //    }
            //    string uploadFolderPath = HttpContext.Current.Server.MapPath("~/Upload");

            //    //如果路径不存在，创建路径
            //    if (!Directory.Exists(uploadFolderPath))
            //    {
            //        Directory.CreateDirectory(uploadFolderPath);
            //    }
            //    List<string> files = new List<string>();
            //    string guid = Guid.NewGuid().ToString("N");
            //    var provider = new WithExtensionMultipartFormDataStreamProvider(uploadFolderPath, guid);

            //    await Request.Content.ReadAsMultipartAsync(provider);
            //    string url = "";
            //    foreach (var file in provider.FileData)
            //    {
            //        files.Add(Path.GetFileName(file.LocalFileName));
            //        url += "/Upload/" + Path.GetFileName(file.LocalFileName) + "|";
            //    }
            //    url = url.Substring(0, url.Length - 1);
            //    TB_Users user = new TB_Users();
            //    user.user_id = user_id;
            //    user.url = url;
            //    ModelState.Remove("user_name");
            //    ModelState.Remove("user_password");
            //    ModelState.Remove("fullname");
            //    bool ss = us.UpdateEntity(user);
            //    result.Code = "200";
            //    result.Msg = "上传成功!";
            //    result.Data = "";
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var validationErrors in e.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            //   logger.Error("UploadAvatar:" + validationErrors.Entry.Entity.GetType().FullName + "," + validationError.PropertyName + "," + validationError.ErrorMessage);
            //        }
            //    }
            //    result.Code = "500";
            //    result.Msg = e.Message;
            //    //   logger.Error(e.Message);
            //}
            //return result;
            HttpRequest request = HttpContext.Current.Request;
            HttpFileCollection fileCollection = request.Files;

            return us.UploadAvatar(user_id, fileCollection);
        }

        /// <summary>
        /// 存储过程查询
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Result P_User(int user_id) {
            return us.P_User(user_id);
        }


        /// <summary>
        /// 自定义执行sql语句
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result P_EdtUser(int user_id)
        {
            return us.P_EdtUser(user_id);
        }

        /// <summary>
        /// 获取用户角色名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public Result GetUserRoleName() {
            return us.GetUserRoleName();
        }


    }
}
