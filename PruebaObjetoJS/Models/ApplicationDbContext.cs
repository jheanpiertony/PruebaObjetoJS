using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PruebaObjetoJS.Models
{
    public class ApplicationDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApplicationDbContext() : base("name=ApplicationDbContext")
        {
        }
        public  DbSet<Clientes> Clientes { get; set; }
        public  DbSet<Documentos> Documentos { get; set; }
        public  DbSet<TipoDocumentos> TipoDocumentos { get; set; }
        public  DbSet<CorreoElectronicos> CorreoElectronicos { get; set; }
        public  DbSet<TipoCorreos> TipoCorreos { get; set; }
        public  DbSet<Direcciones> Direcciones { get; set; }
        public  DbSet<PlantillaDocumentos> PlantillaDocumentos { get; set; }
        public  DbSet<ClientesDocumentoJuridicos> ClientesDocumentoJuridicos { get; set; }
        public  DbSet<Empresas> Empresas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            //modelBuilder.Properties<decimal>().Configure(x => x.HasColumnType("decimal").HasPrecision(16, 2));
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
