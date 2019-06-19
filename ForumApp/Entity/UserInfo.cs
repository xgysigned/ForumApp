using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumApp.Entity
{
    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// 头像
        /// </summary>
        public string headPicture { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public RoleInfo role { get; set; }
    }
}