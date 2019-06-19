using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumApp.Entity
{
    public class RoleInfo
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 角色类型
        /// </summary>
        public string type { get; set; }
    }
}