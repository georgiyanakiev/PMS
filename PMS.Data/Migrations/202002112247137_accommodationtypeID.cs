namespace PMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accommodationtypeID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccommodationPackages", "AccommodationTypeID", "dbo.AccommodationTypes");
            DropIndex("dbo.AccommodationPackages", new[] { "AccommodationTypeID" });
            AlterColumn("dbo.AccommodationPackages", "AccommodationTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.AccommodationPackages", "AccommodationTypeID");
            AddForeignKey("dbo.AccommodationPackages", "AccommodationTypeID", "dbo.AccommodationTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccommodationPackages", "AccommodationTypeID", "dbo.AccommodationTypes");
            DropIndex("dbo.AccommodationPackages", new[] { "AccommodationTypeID" });
            AlterColumn("dbo.AccommodationPackages", "AccommodationTypeID", c => c.Int());
            CreateIndex("dbo.AccommodationPackages", "AccommodationTypeID");
            AddForeignKey("dbo.AccommodationPackages", "AccommodationTypeID", "dbo.AccommodationTypes", "ID");
        }
    }
}
