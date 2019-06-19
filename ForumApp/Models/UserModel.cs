using ForumApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApp.Models
{
    public class UserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime lastLoginTime { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string role { get; set; }
    }
}