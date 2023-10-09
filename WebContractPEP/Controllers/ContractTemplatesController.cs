using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using WebContractPEP.Models;


namespace WebContractPEP.Controllers
{
    public class ContractTemplatesController : Controller
    {
        private ContractContext db = new ContractContext();

        // GET: ContractTemplates
        public ActionResult Index()
        {
            return View(db.Templates.ToList());
        }

        // GET: ContractTemplates/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractTemplate contractTemplate = db.Templates.Find(id);
            if (contractTemplate == null)
            {
                return HttpNotFound();
            }
            return View(contractTemplate);
        }

        // GET: ContractTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractTemplates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactTemplateId,IsActive,Name")] ContractTemplate contractTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Templates.Add(contractTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractTemplate);
        }

        // GET: ContractTemplates/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractTemplate contractTemplate = db.Templates.Find(id);
            if (contractTemplate == null)
            {
                return HttpNotFound();
            }
            return View(contractTemplate);
        }

        // POST: ContractTemplates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactTemplateId,IsActive,Name")] ContractTemplate contractTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractTemplate);
        }

        // GET: ContractTemplates/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractTemplate contractTemplate = db.Templates.Find(id);
            if (contractTemplate == null)
            {
                return HttpNotFound();
            }
            return View(contractTemplate);
        }

        // POST: ContractTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContractTemplate contractTemplate = db.Templates.Find(id);
            db.Templates.Remove(contractTemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            string folder = "~/Files/";
            if (!Directory.Exists(Server.MapPath(folder)))
            {
                {
                    Directory.CreateDirectory(Server.MapPath(folder));
                }
            }

            string path = string.Empty;
            object fileSavePath = null;
            ContractTemplate template = new ContractTemplate();
            List<string> sampleList = new List<string>();
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                path = folder + fileName;
                fileSavePath = Server.MapPath(path);
                upload.SaveAs(filename: fileSavePath.ToString());
                upload.InputStream.Close();


                _Application applicationclass = new Application();
                applicationclass.Documents.Open(ref fileSavePath);
                applicationclass.Visible = false;
                Document document = applicationclass.ActiveDocument;


                for (int i = 1; i <= document.Words.Count; i++)
                {
                    sampleList.Add(document.Words[i].Text);

                }
               
                template.Name = fileName;
                template.FinalText = sampleList;


                document.Close();
                //Delete the Uploaded Word File.

                // ViewBag.template = template;
                System.IO.File.Delete(fileSavePath.ToString());
            }
            else
            {
                return RedirectToAction("View", "ContractTemplates", ViewBag); // нет файла для загрузки, обработать //ToDo
            }
            db.Templates.Add(template);
            db.SaveChanges();
            long id = template.ContactTemplateId;
            ViewData["id"] = id;
            ViewBag.id = id;
            TempData["id"] = id;
            HttpContext.Session["id"] = id;
            ViewBag.message = id;
            return RedirectToAction("Details", "ContractTemplates", new { id });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
