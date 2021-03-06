﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RESTClient
    {
        // 常量
        private const string HEAD_KEY = "";
        private const string TOKEN_KEY_PREFIX = "";

        // Restful访问基地址
        private string baseUrl { get; set; }
        // 令牌钥匙
        private string tokenKey { get; set; }
        // 访问超时时间
        private long timeout { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RESTClient()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// 请求GET方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RESTResponseEntity RequestGet(string url)
        {
            RESTResponseEntity entity = new RESTResponseEntity();
            try
            {
                HttpClient httpClient = new HttpClient();
                if (timeout > 0)
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                }
                //httpClient.DefaultRequestHeaders.Add(HEAD_KEY, TOKEN_KEY_PREFIX + " " + tokenKey);
                string strUrl = baseUrl + url;
                HttpResponseMessage hrm = httpClient.GetAsync(strUrl).Result;
                entity.StatusCode = Convert.ToInt32(hrm.StatusCode);
                entity.Buffer = hrm.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }

        /// <summary>
        /// post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content">  HttpContent content = new StringContent("{a:1,b:2}", Encoding.UTF8, "application/json");</param>
        /// <returns></returns>
        public RESTResponseEntity RequestPOST(string url, HttpContent content)
        {
            RESTResponseEntity entity = new RESTResponseEntity();
            HttpClient httpClient = null;

            HttpResponseMessage hrm = null;
            try
            {
                httpClient = new HttpClient();
                if (timeout > 0)
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                }
                //  httpClient.DefaultRequestHeaders.Add(HEAD_KEY, TOKEN_KEY_PREFIX + " " + tokenKey);
                string strUrl = baseUrl + url;
                hrm = httpClient.PostAsync(strUrl, content).Result;
                entity.StatusCode = Convert.ToInt32(hrm.StatusCode);
                entity.Buffer = hrm.Content.ReadAsByteArrayAsync().Result;
                entity.Data = hrm.Content.ReadAsStringAsync().Result;
                httpClient.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
            if (content != null)
            {
                content.Dispose();
                content = null;
            }
            if (hrm != null)
            {
                hrm.Dispose();
                hrm = null;
            }

            return entity;
        }

        /// <summary>
        /// 请求PUT方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content">  HttpContent content = new StringContent("{a:1,b:2}", Encoding.UTF8, "application/json");</param>
        /// <returns></returns>
        public RESTResponseEntity RequestPUT(string url, HttpContent content)
        {
            RESTResponseEntity entity = new RESTResponseEntity();
            try
            {
                HttpClient httpClient = new HttpClient();
                if (timeout > 0)
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                }
                //httpClient.DefaultRequestHeaders.Add(HEAD_KEY, TOKEN_KEY_PREFIX + " " + tokenKey);
                string strUrl = baseUrl + url;
                HttpResponseMessage hrm = httpClient.PutAsync(strUrl, content).Result;
                entity.StatusCode = Convert.ToInt32(hrm.StatusCode);
                entity.Buffer = hrm.Content.ReadAsByteArrayAsync().Result;
                entity.Data = hrm.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }

        /// <summary>
        /// 请求DELETE方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RESTResponseEntity RequestDELETE(string url)
        {
            RESTResponseEntity entity = new RESTResponseEntity();
            try
            {
                HttpClient httpClient = new HttpClient();
                if (timeout > 0)
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                }
                httpClient.DefaultRequestHeaders.Add(HEAD_KEY, TOKEN_KEY_PREFIX + " " + tokenKey);
                string strUrl = baseUrl + url;
                HttpResponseMessage hrm = httpClient.DeleteAsync(strUrl).Result;
                entity.StatusCode = Convert.ToInt32(hrm.StatusCode);
                entity.Buffer = hrm.Content.ReadAsByteArrayAsync().Result;
                entity.Data = hrm.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }

    }
}
