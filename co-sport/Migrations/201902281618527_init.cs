namespace co_sport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Abstract = c.String(),
                        Manager_StuNum = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.User", t => t.Manager_StuNum)
                .Index(t => t.Manager_StuNum);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        StuNum = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Contact = c.String(),
                        WeChatID = c.String(),
                    })
                .PrimaryKey(t => t.StuNum);
            
            CreateTable(
                "dbo.SportTime",
                c => new
                    {
                        SportTimeID = c.Guid(nullable: false),
                        TimeID = c.Int(nullable: false),
                        User_StuNum = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SportTimeID)
                .ForeignKey("dbo.User", t => t.User_StuNum, cascadeDelete: true)
                .Index(t => t.User_StuNum);
            
            CreateTable(
                "dbo.Invitation",
                c => new
                    {
                        InvitationID = c.Guid(nullable: false),
                        Agreed = c.Boolean(),
                        StuNum = c.String(),
                        GroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InvitationID);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        RequestID = c.Guid(nullable: false),
                        Agreed = c.Boolean(),
                        StuNum = c.String(),
                        GroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RequestID);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        User_StuNum = c.String(nullable: false, maxLength: 128),
                        Group_GroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_StuNum, t.Group_GroupID })
                .ForeignKey("dbo.User", t => t.User_StuNum, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.Group_GroupID, cascadeDelete: true)
                .Index(t => t.User_StuNum)
                .Index(t => t.Group_GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Group", "Manager_StuNum", "dbo.User");
            DropForeignKey("dbo.SportTime", "User_StuNum", "dbo.User");
            DropForeignKey("dbo.UserGroup", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.UserGroup", "User_StuNum", "dbo.User");
            DropIndex("dbo.UserGroup", new[] { "Group_GroupID" });
            DropIndex("dbo.UserGroup", new[] { "User_StuNum" });
            DropIndex("dbo.SportTime", new[] { "User_StuNum" });
            DropIndex("dbo.Group", new[] { "Manager_StuNum" });
            DropTable("dbo.UserGroup");
            DropTable("dbo.Request");
            DropTable("dbo.Invitation");
            DropTable("dbo.SportTime");
            DropTable("dbo.User");
            DropTable("dbo.Group");
        }
    }
}
