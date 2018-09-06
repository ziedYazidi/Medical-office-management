namespace GMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableCalendar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarEvents",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CalendarEvents");
        }
    }
}
