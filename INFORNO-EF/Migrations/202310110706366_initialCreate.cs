namespace INFORNO_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dettagli",
                c => new
                    {
                        IdDettaglio = c.Int(nullable: false, identity: true),
                        FKPizza = c.Int(nullable: false),
                        Quantita = c.Int(),
                        FKOrdine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDettaglio)
                .ForeignKey("dbo.Ordini", t => t.FKOrdine)
                .ForeignKey("dbo.Pizze", t => t.FKPizza)
                .Index(t => t.FKPizza)
                .Index(t => t.FKOrdine);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        IndirizzoSpedizione = c.String(nullable: false, maxLength: 100),
                        Note = c.String(maxLength: 200),
                        FKUtente = c.Int(nullable: false),
                        ImportoTotale = c.Decimal(nullable: false, storeType: "money"),
                        Concluso = c.Boolean(),
                        Evaso = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.Utenti", t => t.FKUtente)
                .Index(t => t.FKUtente);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        IdUtente = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        IsAdmin = c.Boolean(),
                    })
                .PrimaryKey(t => t.IdUtente);
            
            CreateTable(
                "dbo.Pizze",
                c => new
                    {
                        IdPizza = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Foto = c.String(maxLength: 50),
                        Prezzo = c.String(nullable: false, maxLength: 50),
                        TempoConsegna = c.String(maxLength: 50),
                        Ingredienti = c.String(maxLength: 200),
                        Quantita = c.Int(),
                    })
                .PrimaryKey(t => t.IdPizza);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dettagli", "FKPizza", "dbo.Pizze");
            DropForeignKey("dbo.Ordini", "FKUtente", "dbo.Utenti");
            DropForeignKey("dbo.Dettagli", "FKOrdine", "dbo.Ordini");
            DropIndex("dbo.Ordini", new[] { "FKUtente" });
            DropIndex("dbo.Dettagli", new[] { "FKOrdine" });
            DropIndex("dbo.Dettagli", new[] { "FKPizza" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Pizze");
            DropTable("dbo.Utenti");
            DropTable("dbo.Ordini");
            DropTable("dbo.Dettagli");
        }
    }
}
