using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PruebaObjetoJS.Models;

namespace PruebaObjetoJS.Controllers
{
    public class ClientesDocumentoJuridicosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientesDocumentoJuridicos
        public ActionResult Index()
        {
            var clientesDocumentoJuridicos = db.ClientesDocumentoJuridicos.Include(c => c.Clientes).Include(c => c.Empresas).Include(c => c.PlantillaDocumentos);
            return View(clientesDocumentoJuridicos.ToList());
        }

        // GET: ClientesDocumentoJuridicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesDocumentoJuridicos clientesDocumentoJuridicos = db.ClientesDocumentoJuridicos.Find(id);
            if (clientesDocumentoJuridicos == null)
            {
                return HttpNotFound();
            }
            return View(clientesDocumentoJuridicos);
        }

        // GET: ClientesDocumentoJuridicos/Create
        public ActionResult Create()
        {
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "PrimerNombre");
            ViewBag.EmpresasId = new SelectList(db.Empresas, "Id", "Empresa");
            ViewBag.PlantillaDocumentosId = new SelectList(db.PlantillaDocumentos, "Id", "Plantilla");

            JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            List<StringJson> _stringAux = StringAux(db.Clientes
                .Include(d => d.Documentos.Select(t => t.TipoDocumentos))
                .Include(c => c.CorreoElectronicos.Select(t => t.TipoCorreos))
                .Include(d => d.Direcciones).FirstOrDefault(x => x.Id == 1));

            ViewBag._cliente = JsonConvert.SerializeObject(_stringAux, _settings);

            return View();
        }

        // POST: ClientesDocumentoJuridicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DocumentoTexto,Resumen,ClientesId,PlantillaDocumentosId,EmpresasId,FechaCreacion,FechaActualizacion")] ClientesDocumentoJuridicos clientesDocumentoJuridicos)
        {
            if (ModelState.IsValid)
            {
                db.ClientesDocumentoJuridicos.Add(clientesDocumentoJuridicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "PrimerNombre", clientesDocumentoJuridicos.ClientesId);
            ViewBag.EmpresasId = new SelectList(db.Empresas, "Id", "Empresa", clientesDocumentoJuridicos.EmpresasId);
            ViewBag.PlantillaDocumentosId = new SelectList(db.PlantillaDocumentos, "Id", "Plantilla", clientesDocumentoJuridicos.PlantillaDocumentosId);
            return View(clientesDocumentoJuridicos);
        }

        // GET: ClientesDocumentoJuridicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesDocumentoJuridicos clientesDocumentoJuridicos = db.ClientesDocumentoJuridicos.Find(id);
            if (clientesDocumentoJuridicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "PrimerNombre", clientesDocumentoJuridicos.ClientesId);
            ViewBag.EmpresasId = new SelectList(db.Empresas, "Id", "Empresa", clientesDocumentoJuridicos.EmpresasId);
            ViewBag.PlantillaDocumentosId = new SelectList(db.PlantillaDocumentos, "Id", "Plantilla", clientesDocumentoJuridicos.PlantillaDocumentosId);
            return View(clientesDocumentoJuridicos);
        }

        // POST: ClientesDocumentoJuridicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DocumentoTexto,Resumen,ClientesId,PlantillaDocumentosId,EmpresasId,FechaCreacion,FechaActualizacion")] ClientesDocumentoJuridicos clientesDocumentoJuridicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientesDocumentoJuridicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "PrimerNombre", clientesDocumentoJuridicos.ClientesId);
            ViewBag.EmpresasId = new SelectList(db.Empresas, "Id", "Empresa", clientesDocumentoJuridicos.EmpresasId);
            ViewBag.PlantillaDocumentosId = new SelectList(db.PlantillaDocumentos, "Id", "Plantilla", clientesDocumentoJuridicos.PlantillaDocumentosId);
            return View(clientesDocumentoJuridicos);
        }

        // GET: ClientesDocumentoJuridicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesDocumentoJuridicos clientesDocumentoJuridicos = db.ClientesDocumentoJuridicos.Find(id);
            if (clientesDocumentoJuridicos == null)
            {
                return HttpNotFound();
            }
            return View(clientesDocumentoJuridicos);
        }

        // POST: ClientesDocumentoJuridicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientesDocumentoJuridicos clientesDocumentoJuridicos = db.ClientesDocumentoJuridicos.Find(id);
            db.ClientesDocumentoJuridicos.Remove(clientesDocumentoJuridicos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
                CorreoElectronico = clientes.CorreoElectronicos.Select(c => c.CorreoElectronico).FirstOrDefault().ToString()
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
                if (item.TipoDocumento == "CC")
                {
                    return item.TipoDocumento.ToString();
                }
            }

            return "";
        }
        #endregion
    }
}
