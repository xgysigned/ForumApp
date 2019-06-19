namespace ForumApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        nickName = c.String(),
                        password = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        createTime = c.DateTime(nullable: false),
                        lastLoginTime = c.DateTime(nullable: false),
                        headPicture = c.String(),
                        sex = c.Int(nullable: false),
                        role_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.RoleInfoes", t => t.role_id)
                .Index(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "role_id", "dbo.RoleInfoes");
            DropIndex("dbo.UserInfoes", new[] { "role_id" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.RoleInfoes");
        }
    }
}
