//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service;
using Model;

namespace WebAPI.Controllers
{
    public partial class UploadFileController: ApiController
		{
		 public UploadFileService uploadfileService { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="uploadfile"></param>
        /// <returns></returns>
		[HttpPost]
        public Result Add(UploadFile uploadfile)
        {
            return uploadfileService.Add(uploadfile);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="uploadfile"></param>
        /// <returns></returns>
		[HttpPost]
        public Result Update(UploadFile uploadfile)
        {
            return uploadfileService.Update(uploadfile);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="uploadfile"></param>
        /// <returns></returns>
		[HttpPost]
        public Result Delete(UploadFile uploadfile)
        {
            return uploadfileService.Delete(uploadfile);
        }
		}
}
