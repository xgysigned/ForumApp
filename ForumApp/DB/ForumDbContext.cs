using ForumApp.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ForumApp.DB
{
    public class ForumDbContext:DbContext
    {
        public ForumDbContext():base("name=ForumAppDB")
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }

    }
}