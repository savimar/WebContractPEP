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


                Body body = null;
                MainDocumentPart mainPart = null;
                using (var wordDocument = WordprocessingDocument.Open(fileSavePath as string, false))
                {
                    mainPart = wordDocument.MainDocumentPart;
                    body = wordDocument.MainDocumentPart.Document.Body;
                    if (body != null)
                    {
                        FinalText = ConvertWordToHTML(body, mainPart);
                    }
                   
                }

              

                template.Name = fileName;
                template.FinalText = FinalText;


               
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
            //TempData["text"] = FinalText;
           /* ViewData["id"] = id;
            ViewBag.id = id;
            TempData["id"] = id;
            HttpContext.Session["id"] = id;
            ViewBag.message = id;
           */
            return RedirectToAction("Details", "ContractTemplates",new { id });
        }
        private string ConvertWordToHTML(Body content, MainDocumentPart wDoc)
        {
            string htmlConvertedString = string.Empty;
            foreach (Paragraph par in content.Descendants<Paragraph>())
            {
                foreach (Run run in par.Descendants<Run>())
                {
                    RunProperties props = run.RunProperties;
                    htmlConvertedString += ApplyTextFormatting(run.InnerText, props);
                }
            }
            return htmlConvertedString;
        }
        private string ApplyTextFormatting(string content, RunProperties property)
        {
            StringBuilder buildString = new StringBuilder(content);

            if (property?.Bold != null)
            {
                buildString.Insert(0, "<b>");
                buildString.Append("</b>");
            }

            if (property?.Italic != null)
            {
                buildString.Insert(0, "<i>");
                buildString.Append("</i>");
            }

            if (property?.Underline != null)
            {
                buildString.Insert(0, "<u>");
                buildString.Append("</u>");
            }

            if (property?.Color != null && property.Color.Val != null)
            {
                buildString.Insert(0, "<span style=\"color: #" + property.Color.Val + "\">");
                buildString.Append("</span>");
            }

            if (property?.Highlight != null && property.Highlight.Val != null)
            {
                buildString.Insert(0, "<span style=\"background-color: " + property.Highlight.Val + "\">");
                buildString.Append("</span>");
            }

            if (property?.Strike != null)
            {
                buildString.Insert(0, "<s>");
                buildString.Append("</s>");
            }

            return buildString.ToString();

        }
        public string GetPlainText(OpenXmlElement element)
        {
            StringBuilder PlainTextInWord = new StringBuilder();
            foreach (OpenXmlElement section in element.Elements())
            {
                switch (section.LocalName)
                {
                    // Space 
                    case " ":
                        PlainTextInWord.Append("\u0020");
                        break;
                    // Text 
                    case "t":
                        PlainTextInWord.Append(section.InnerText);
                        break;


                    case "cr":                          // Carriage return 
                    case "br":                          // Page break 
                        PlainTextInWord.Append(Environment.NewLine);
                        break;


                    // Tab 
                    case "tab":
                        PlainTextInWord.Append("\t");
                        break;


                    // Paragraph 
                    case "p":
                        PlainTextInWord.Append(GetPlainText(section));
                        PlainTextInWord.AppendLine(Environment.NewLine);
                        break;
                    


                    default:
                        PlainTextInWord.Append(GetPlainText(section));
                        break;
                }
            }


            return PlainTextInWord.ToString();
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
