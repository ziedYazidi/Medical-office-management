namespace GMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RendezVous",
                c => new
                    {
                        RendezVousId = c.Int(nullable: false),
                        ActeMedicalId = c.Int(nullable: false),
                        DateRendezVous = c.String(),
                        AdresseRendezVous = c.String(),
                    })
                .PrimaryKey(t => t.RendezVousId)
                .ForeignKey("dbo.ActeMedicals", t => t.RendezVousId)
                .Index(t => t.RendezVousId);
            
            CreateTable(
                "dbo.ActeMedicals",
                c => new
                    {
                        ActeMedicalId = c.Int(nullable: false, identity: true),
                        DMDPatientId = c.Int(nullable: false),
                        FactureId = c.Int(nullable: false),
                        RendezVousId = c.Int(nullable: false),
                        Traitement = c.String(),
                        ConsultationId = c.Int(),
                        MotifConsultation = c.String(),
                        HistoireMaladie = c.String(),
                        ExamenClinique = c.String(),
                        DiagnostiqueFinal = c.String(),
                        traitementPropose = c.String(),
                        OperationId = c.Int(),
                        AdresseOperation = c.String(),
                        IntituleOperation = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ActeMedicalId)
                .ForeignKey("dbo.DMDPatients", t => t.DMDPatientId, cascadeDelete: true)
                .Index(t => t.DMDPatientId);
            
            CreateTable(
                "dbo.DMDPatients",
                c => new
                    {
                        DMDPatientId = c.Int(nullable: false, identity: true),
                        NomPatient = c.String(),
                        PrenomPatient = c.String(),
                        TelPatient = c.Int(nullable: false),
                        MailPatient = c.String(),
                        AdressePatient = c.String(),
                        AgePatient = c.String(),
                        AntecedentsFamiliaux = c.String(),
                        AntecedentsPersonnel = c.String(),
                        CompagnieAssurence = c.String(),
                    })
                .PrimaryKey(t => t.DMDPatientId);
            
            CreateTable(
                "dbo.DocumentComplementaires",
                c => new
                    {
                        DocumentComplementaireId = c.Int(nullable: false, identity: true),
                        ActeMedicalId = c.Int(nullable: false),
                        TypeDocumnet = c.String(),
                        TitreDocument = c.String(),
                        contenuDocument = c.String(),
                        Avis = c.String(),
                    })
                .PrimaryKey(t => t.DocumentComplementaireId)
                .ForeignKey("dbo.ActeMedicals", t => t.ActeMedicalId, cascadeDelete: true)
                .Index(t => t.ActeMedicalId);
            
            CreateTable(
                "dbo.Factures",
                c => new
                    {
                        FactureId = c.Int(nullable: false),
                        ActeMedicalId = c.Int(nullable: false),
                        EtatReglement = c.Boolean(nullable: false),
                        Montant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactureId)
                .ForeignKey("dbo.ActeMedicals", t => t.FactureId)
                .Index(t => t.FactureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RendezVous", "RendezVousId", "dbo.ActeMedicals");
            DropForeignKey("dbo.Factures", "FactureId", "dbo.ActeMedicals");
            DropForeignKey("dbo.DocumentComplementaires", "ActeMedicalId", "dbo.ActeMedicals");
            DropForeignKey("dbo.ActeMedicals", "DMDPatientId", "dbo.DMDPatients");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Factures", new[] { "FactureId" });
            DropIndex("dbo.DocumentComplementaires", new[] { "ActeMedicalId" });
            DropIndex("dbo.ActeMedicals", new[] { "DMDPatientId" });
            DropIndex("dbo.RendezVous", new[] { "RendezVousId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Factures");
            DropTable("dbo.DocumentComplementaires");
            DropTable("dbo.DMDPatients");
            DropTable("dbo.ActeMedicals");
            DropTable("dbo.RendezVous");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
