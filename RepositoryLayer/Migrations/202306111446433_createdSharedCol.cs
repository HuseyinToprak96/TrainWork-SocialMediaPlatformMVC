namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdSharedCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shareds", "CreatedUserId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shareds", "CreatedUserId");
        }
    }
}
