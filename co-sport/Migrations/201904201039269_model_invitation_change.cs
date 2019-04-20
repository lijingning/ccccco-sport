namespace co_sport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_invitation_change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitation", "GroupName", c => c.String());
            AddColumn("dbo.Request", "StuName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "StuName");
            DropColumn("dbo.Invitation", "GroupName");
        }
    }
}
