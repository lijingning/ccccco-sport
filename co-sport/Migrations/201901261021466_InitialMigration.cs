namespace co_sport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Guid(nullable: false),
                        Name = c.String(),
                        Manager_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.User", t => t.Manager_UserID)
                .Index(t => t.Manager_UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Gender = c.Boolean(nullable: false),
                        StuNum = c.Int(nullable: false),
                        Name = c.String(),
                        Contact = c.String(),
                        SportTimeTable_SportTimeTableID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.SportTimeTable", t => t.SportTimeTable_SportTimeTableID)
                .Index(t => t.SportTimeTable_SportTimeTableID);
            
            CreateTable(
                "dbo.SportTimeTable",
                c => new
                    {
                        SportTimeTableID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SportTimeTableID);
            
            CreateTable(
                "dbo.Time",
                c => new
                    {
                        TimeID = c.Int(nullable: false, identity: true),
                        SportTimeTable_SportTimeTableID = c.Guid(),
                    })
                .PrimaryKey(t => t.TimeID)
                .ForeignKey("dbo.SportTimeTable", t => t.SportTimeTable_SportTimeTableID)
                .Index(t => t.SportTimeTable_SportTimeTableID);
            
            CreateTable(
                "dbo.Invitation",
                c => new
                    {
                        InvitationID = c.Guid(nullable: false),
                        Agreed = c.Boolean(),
                        Inviter_GroupID = c.Guid(),
                        Receptor_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.InvitationID)
                .ForeignKey("dbo.Group", t => t.Inviter_GroupID)
                .ForeignKey("dbo.User", t => t.Receptor_UserID)
                .Index(t => t.Inviter_GroupID)
                .Index(t => t.Receptor_UserID);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Group_GroupID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Group_GroupID })
                .ForeignKey("dbo.User", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.Group_GroupID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Group_GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invitation", "Receptor_UserID", "dbo.User");
            DropForeignKey("dbo.Invitation", "Inviter_GroupID", "dbo.Group");
            DropForeignKey("dbo.Group", "Manager_UserID", "dbo.User");
            DropForeignKey("dbo.User", "SportTimeTable_SportTimeTableID", "dbo.SportTimeTable");
            DropForeignKey("dbo.Time", "SportTimeTable_SportTimeTableID", "dbo.SportTimeTable");
            DropForeignKey("dbo.UserGroup", "Group_GroupID", "dbo.Group");
            DropForeignKey("dbo.UserGroup", "User_UserID", "dbo.User");
            DropIndex("dbo.UserGroup", new[] { "Group_GroupID" });
            DropIndex("dbo.UserGroup", new[] { "User_UserID" });
            DropIndex("dbo.Invitation", new[] { "Receptor_UserID" });
            DropIndex("dbo.Invitation", new[] { "Inviter_GroupID" });
            DropIndex("dbo.Time", new[] { "SportTimeTable_SportTimeTableID" });
            DropIndex("dbo.User", new[] { "SportTimeTable_SportTimeTableID" });
            DropIndex("dbo.Group", new[] { "Manager_UserID" });
            DropTable("dbo.UserGroup");
            DropTable("dbo.Invitation");
            DropTable("dbo.Time");
            DropTable("dbo.SportTimeTable");
            DropTable("dbo.User");
            DropTable("dbo.Group");
        }
    }
}
