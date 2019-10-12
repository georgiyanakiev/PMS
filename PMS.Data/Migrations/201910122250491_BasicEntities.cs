namespace PMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationPackages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationTypeID = c.Int(nullable: false),
                        Name = c.String(),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccommodationTypeID, cascadeDelete: true)
                .Index(t => t.AccommodationTypeID);
            
            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Descripttion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationPackageID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccommodationPackages", t => t.AccommodationPackageID, cascadeDelete: true)
                .Index(t => t.AccommodationPackageID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccommodationID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationID, cascadeDelete: true)
                .Index(t => t.AccommodationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "AccommodationID", "dbo.Accommodations");
            DropForeignKey("dbo.Accommodations", "AccommodationPackageID", "dbo.AccommodationPackages");
            DropForeignKey("dbo.AccommodationPackages", "AccommodationTypeID", "dbo.AccommodationTypes");
            DropIndex("dbo.Bookings", new[] { "AccommodationID" });
            DropIndex("dbo.Accommodations", new[] { "AccommodationPackageID" });
            DropIndex("dbo.AccommodationPackages", new[] { "AccommodationTypeID" });
            DropTable("dbo.Bookings");
            DropTable("dbo.Accommodations");
            DropTable("dbo.AccommodationTypes");
            DropTable("dbo.AccommodationPackages");
        }
    }
}
