using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApp.Util
{
    public class ResultMode
    {
        /// <summary>
        /// 1表示成功、-1表示失败
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 记录成功或失败的信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public string Data { get; set; }
    }
}