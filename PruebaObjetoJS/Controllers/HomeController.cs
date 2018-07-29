using PruebaObjetoJS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;

namespace PruebaObjetoJS.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,   
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            List<StringJson> _stringAux = StringAux(db.Clientes
                .Include(d => d.Documentos.Select(t => t.TipoDocumentos))
                .Include(c => c.CorreoElectronicos.Select(t => t.TipoCorreos))
                .Include(d => d.Direcciones).FirstOrDefault(x => x.Id ==1));
            
            ViewBag._cliente = JsonConvert.SerializeObject(_stringAux, _settings);
            return View();
        }

        #region FuncionesUtiles      
        private List<StringJson> StringAux(Clientes clientes)
        {
            int _nroPropiedades = typeof(Clientes).GetProperties().Count();  
            int i = 0;

            List<StringJson> _stringJson = new List<StringJson>()
            {
                new StringJson
                {
                    Id= i++,
                    Nombre= nameof(clientes.PrimerNombre).ToString(),
                    Descripcion= clientes.PrimerNombre,
                    Valor= clientes.PrimerNombre,
                    NombrePropiedad= nameof(clientes.PrimerNombre).ToString()
                },
                new StringJson
                {
                    Id= i++,
                    Nombre= nameof(clientes.SegundoNombre).ToString(),
                    Descripcion= clientes.SegundoNombre,
                    Valor= clientes.SegundoNombre,
                    NombrePropiedad= nameof(clientes.SegundoNombre).ToString()
                }
            };
            return _stringJson;
        }

        private AuxClientes CargarDatosClientes(Clientes clientes)
        {
            AuxClientes _cliente = new AuxClientes
            {
                Id = clientes.Id,
                PrimerNombre = clientes.PrimerNombre,
                SegundoNombre = clientes.SegundoNombre,
                PrimerApellido = clientes.PrimerApellido,
                SegundoApellido = clientes.SegundoApellido,
                FechaNacimiento = clientes.FechaNacimiento,
                TipoDocumento = clientes.Documentos.Select(t => t.TipoDocumentos).FirstOrDefault().TipoDocumento,
                NroDocumento = clientes.Documentos.Select(n => n.NroDocumento).FirstOrDefault(),
                LugarExpedicion = clientes.Documentos.Select(l => l.LugarExpedicion).FirstOrDefault(),
                FechaExpedicion = clientes.Documentos.Select(f => f.FechaExpedicion).FirstOrDefault(),
                Nacionalidad = clientes.Documentos.Select(n => n.Nacionalidad).FirstOrDefault(),
                Direccion = clientes.Direcciones.Direccion,
                Ciudad = clientes.Direcciones.Ciudad,
                Departamento = clientes.Direcciones.Departamento,
                Pais = clientes.Direcciones.Pais,
                CodeZip = clientes.Direcciones.CodeZip,
                CorreoElectronico= clientes.CorreoElectronicos.Select(c=> c.CorreoElectronico).FirstOrDefault().ToString()       
            };

             //string _JsonString = nameof(_cliente.FechaExpedicion).ToString();
            //int _nropro= typeof(Clientes).GetProperties().Count();
            //int _nropro2 = typeof(Clientes).GetProperties().Length;
            //var isVirtual = typeof(Clientes).GetProperty(nameof(clientes.Direcciones).ToString()).GetGetMethod().IsVirtual;
            //var isVirtual2=typeof(Clientes).GetProperty(nameof(clientes.Direcciones).ToString()).GetAccessors()[0].IsVirtual;
            return _cliente;
        }

        private string _buscarTipoDocumento(List<TipoDocumentos> _tipoDocumentos)
        {
            foreach (var item in _tipoDocumentos)
            {
                if (item.TipoDocumento=="CC")
                {
                    return item.TipoDocumento.ToString();
                }
            }

            return "";
        }
        #endregion
    }
}