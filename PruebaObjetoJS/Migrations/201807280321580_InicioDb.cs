namespace PruebaObjetoJS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InicioDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimerNombre = c.String(),
                        SegundoNombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        DireccionesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Direcciones", t => t.DireccionesId)
                .Index(t => t.DireccionesId);
            
            CreateTable(
                "dbo.ClientesDocumentoJuridicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentoTexto = c.String(),
                        Resumen = c.String(),
                        ClientesId = c.Int(nullable: false),
                        PlantillaDocumentosId = c.Int(nullable: false),
                        EmpresasId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(),
                        FechaActualizacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClientesId)
                .ForeignKey("dbo.Empresas", t => t.EmpresasId)
                .ForeignKey("dbo.PlantillaDocumentos", t => t.PlantillaDocumentosId)
                .Index(t => t.ClientesId)
                .Index(t => t.PlantillaDocumentosId)
                .Index(t => t.EmpresasId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Empresa = c.String(),
                        DireccionesId = c.Int(nullable: false),
                        CorreoElectronicosId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CorreoElectronicos", t => t.CorreoElectronicosId)
                .ForeignKey("dbo.Direcciones", t => t.DireccionesId)
                .Index(t => t.DireccionesId)
                .Index(t => t.CorreoElectronicosId);
            
            CreateTable(
                "dbo.CorreoElectronicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorreoElectronico = c.String(nullable: false),
                        TipoCorreosId = c.Int(nullable: false),
                        Clientes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoCorreos", t => t.TipoCorreosId)
                .ForeignKey("dbo.Clientes", t => t.Clientes_Id)
                .Index(t => t.TipoCorreosId)
                .Index(t => t.Clientes_Id);
            
            CreateTable(
                "dbo.TipoCorreos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoCorreo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Direcciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pais = c.String(),
                        Departamento = c.String(),
                        Ciudad = c.String(),
                        Direccion = c.String(),
                        CodeZip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlantillaDocumentos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Plantilla = c.String(),
                        Descipcion = c.String(),
                        DocumentoTexto = c.String(),
                        FechaCreacion = c.DateTime(),
                        FechaActualizacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documentos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroDocumento = c.Long(nullable: false),
                        Nacionalidad = c.String(),
                        FechaExpedicion = c.DateTime(nullable: false),
                        LugarExpedicion = c.String(),
                        ClientesId = c.Int(nullable: false),
                        TipoDocumentosId = c.Int(nullable: false),
                        TipoDocumento_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClientesId)
                .ForeignKey("dbo.TipoDocumentos", t => t.TipoDocumento_Id)
                .ForeignKey("dbo.TipoDocumentos", t => t.TipoDocumentosId)
                .Index(t => t.ClientesId)
                .Index(t => t.TipoDocumentosId)
                .Index(t => t.TipoDocumento_Id);
            
            CreateTable(
                "dbo.TipoDocumentos",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TipoDocumento = c.String(),
                        Descripcion = c.String(),
                        Prioridad = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documentos", "TipoDocumentosId", "dbo.TipoDocumentos");
            DropForeignKey("dbo.Documentos", "TipoDocumento_Id", "dbo.TipoDocumentos");
            DropForeignKey("dbo.Documentos", "ClientesId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "DireccionesId", "dbo.Direcciones");
            DropForeignKey("dbo.CorreoElectronicos", "Clientes_Id", "dbo.Clientes");
            DropForeignKey("dbo.ClientesDocumentoJuridicos", "PlantillaDocumentosId", "dbo.PlantillaDocumentos");
            DropForeignKey("dbo.Empresas", "DireccionesId", "dbo.Direcciones");
            DropForeignKey("dbo.Empresas", "CorreoElectronicosId", "dbo.CorreoElectronicos");
            DropForeignKey("dbo.CorreoElectronicos", "TipoCorreosId", "dbo.TipoCorreos");
            DropForeignKey("dbo.ClientesDocumentoJuridicos", "EmpresasId", "dbo.Empresas");
            DropForeignKey("dbo.ClientesDocumentoJuridicos", "ClientesId", "dbo.Clientes");
            DropIndex("dbo.Documentos", new[] { "TipoDocumento_Id" });
            DropIndex("dbo.Documentos", new[] { "TipoDocumentosId" });
            DropIndex("dbo.Documentos", new[] { "ClientesId" });
            DropIndex("dbo.CorreoElectronicos", new[] { "Clientes_Id" });
            DropIndex("dbo.CorreoElectronicos", new[] { "TipoCorreosId" });
            DropIndex("dbo.Empresas", new[] { "CorreoElectronicosId" });
            DropIndex("dbo.Empresas", new[] { "DireccionesId" });
            DropIndex("dbo.ClientesDocumentoJuridicos", new[] { "EmpresasId" });
            DropIndex("dbo.ClientesDocumentoJuridicos", new[] { "PlantillaDocumentosId" });
            DropIndex("dbo.ClientesDocumentoJuridicos", new[] { "ClientesId" });
            DropIndex("dbo.Clientes", new[] { "DireccionesId" });
            DropTable("dbo.TipoDocumentos");
            DropTable("dbo.Documentos");
            DropTable("dbo.PlantillaDocumentos");
            DropTable("dbo.Direcciones");
            DropTable("dbo.TipoCorreos");
            DropTable("dbo.CorreoElectronicos");
            DropTable("dbo.Empresas");
            DropTable("dbo.ClientesDocumentoJuridicos");
            DropTable("dbo.Clientes");
        }
    }
}
