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
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<string> Test(Test test)
        {
            List<string> list = new List<string>() { "1", "2" };
            return list;
        }
        [HttpGet]
        public List<string> Test2(int id, string name)
        {
            List<string> list = new List<string>() { "1", "2" };
            return list;
        }

    }
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
