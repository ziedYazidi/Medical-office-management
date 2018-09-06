namespace GMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateEtAssurance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DMDPatients", "DateDeNaissance", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.DMDPatients", "AgePatient");
            DropColumn("dbo.DMDPatients", "CompagnieAssurence");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DMDPatients", "CompagnieAssurence", c => c.String());
            AddColumn("dbo.DMDPatients", "AgePatient", c => c.String());
            DropColumn("dbo.DMDPatients", "DateDeNaissance");
        }
    }
}
