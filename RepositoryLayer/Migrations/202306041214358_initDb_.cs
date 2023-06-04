namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb_ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 250),
                        TopCommentId = c.Int(nullable: false),
                        SharedId = c.Int(),
                        UserId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shareds", t => t.SharedId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SharedId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Shareds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        Path = c.String(maxLength: 100),
                        UserId = c.Int(),
                        Type = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 120),
                        Username = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Gender = c.Int(nullable: false),
                        Biography = c.String(maxLength: 500),
                        DistrictId = c.Int(),
                        RoleId = c.Int(),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.DistrictId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowerId = c.Int(),
                        FollowingId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .ForeignKey("dbo.Users", t => t.FollowingId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowingId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Content = c.String(nullable: false, maxLength: 500),
                        IsSeen = c.Boolean(nullable: false),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SharedLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SharedId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shareds", t => t.SharedId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SharedId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(maxLength: 100),
                        Description = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfileImages", "UserId", "dbo.Users");
            DropForeignKey("dbo.SharedLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.SharedLikes", "SharedId", "dbo.Shareds");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Follows", "FollowingId", "dbo.Users");
            DropForeignKey("dbo.Follows", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "SharedId", "dbo.Shareds");
            DropForeignKey("dbo.Shareds", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropIndex("dbo.UserProfileImages", new[] { "UserId" });
            DropIndex("dbo.SharedLikes", new[] { "UserId" });
            DropIndex("dbo.SharedLikes", new[] { "SharedId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Follows", new[] { "FollowingId" });
            DropIndex("dbo.Follows", new[] { "FollowerId" });
            DropIndex("dbo.Districts", new[] { "CityId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "DistrictId" });
            DropIndex("dbo.Shareds", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "SharedId" });
            DropTable("dbo.UserProfileImages");
            DropTable("dbo.SharedLikes");
            DropTable("dbo.Notifications");
            DropTable("dbo.Follows");
            DropTable("dbo.Roles");
            DropTable("dbo.Districts");
            DropTable("dbo.Users");
            DropTable("dbo.Shareds");
            DropTable("dbo.Comments");
            DropTable("dbo.Cities");
        }
    }
}
