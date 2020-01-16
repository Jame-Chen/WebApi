using Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>

    public class TestController : ApiController
    {
        public UserService us { get; set; }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public Result Test()
        {
            Result ret = us.Test();
            return ret;
        }

    }
}
