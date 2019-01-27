namespace co_sport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuNumToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "StuNum", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "StuNum", c => c.Int(nullable: false));
        }
    }
}
