using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using WebContractPEP.Models;
using DocumentFormat.OpenXml.Vml.Office;
using OpenXmlPowerTools;
using System.Web.UI.HtmlControls;


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
            string fileSavePath = null;
            ContractTemplate template = new ContractTemplate();
             string FinalText = string.Empty;
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                path = folder + fileName;
                fileSavePath = Server.MapPath(path);
                upload.SaveAs(filename: fileSavePath.ToString());
                upload.InputStream.Close();


                //var sourceDocxFileContent = File.ReadAllBytes("./source.docx");

                FileStream fileStream =
                    new FileStream(fileSavePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
               
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    using (var wordDocument = WordprocessingDocument.Open(memoryStream, true))
                    {
                       
                        WmlToHtmlConverterSettings settings = new WmlToHtmlConverterSettings()
                        {
                            PageTitle = fileName,
                            CssClassPrefix = "pt-",
                            RestrictToSupportedLanguages = true,
                            RestrictToSupportedNumberingFormats = true,
                        };


                        XElement htmlElement = WmlToHtmlConverter.ConvertToHtml(wordDocument, settings);
                        var html = new XDocument(
                            new XDocumentType("html", null, null, null),
                            htmlElement);
                        FinalText = html.ToString(SaveOptions.DisableFormatting);


                    }

                }

                template.Name = fileName;
                template.FinalText = FinalText;



                //Delete the Uploaded Word File.

                // ViewBag.template = template;

                fileStream.Close();
                System.IO.File.Delete(fileSavePath);
            }
            else
            {
                return RedirectToAction("View", "ContractTemplates", ViewBag); // нет файла для загрузки, обработать //ToDo
            }
            db.Templates.Add(template);
            db.SaveChanges();
            long id = template.ContactTemplateId;
            //TempData["text"] = FinalText;
           /* ViewData["id"] = id;
            ViewBag.id = id;
            TempData["id"] = id;
            HttpContext.Session["id"] = id;
            ViewBag.message = id;
           */
            return RedirectToAction("Details", "ContractTemplates",new { id });
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
