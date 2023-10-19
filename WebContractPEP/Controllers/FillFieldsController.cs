using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebContractPEP.Models;

namespace WebContractPEP.Controllers
{
    public class FillFieldsController : Controller
    {
        private ContractContext db = new ContractContext();

        // GET: FillFields
        public ActionResult Index()
        {
            return View(db.Fields.ToList());
        }

        // GET: FillFields/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FillField fillField = db.Fields.Find(id);
            if (fillField == null)
            {
                return HttpNotFound();
            }
            return View(fillField);
        }

        // GET: FillFields/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateWithTemplate(long id)
        { // ContractTemplate template = db.Templates.Find(id);

            /*ViewData["id"] = id;
            ViewBag.id = id;
            TempData["id"] = id;
            HttpContext.Session["TemplatedId"] = id;
            ViewBag.message = id;
            */
            HttpContext.Session["TemplatedId"] = id;
            TempData["id"] = id;
            return RedirectToAction("Create");
        }
        // POST: FillFields/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FieldId,IsActive,FieldName,FieldValue,AutoFieldValue,FieldType,AutoFillFieldType,IsRequired,IsFilledExecutor,IsFilledClient,IsNeedSummInWords,IsAutoFillField, ContractTemplateId")] FillField fillField)
        {
            if (ModelState.IsValid)
            {
                ContractTemplate template = db.Templates.Find(fillField.ContractTemplateId);
                fillField.ContractTemplate = template;
                db.Fields.Add(fillField);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fillField);
        }

        // GET: FillFields/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FillField fillField = db.Fields.Find(id);
            if (fillField == null)
            {
                return HttpNotFound();
            }
            return View(fillField);
        }

        // POST: FillFields/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FieldId,IsActive,FieldName,FieldValue,AutoFieldValue,FieldType,AutoFillFieldType,IsRequired,IsFilledExecutor,IsFilledClient,IsNeedSummInWords,IsAutoFillField")] FillField fillField)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fillField).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fillField); //HttpContext.Current.Request.UserHostAddress получить адрес пользователя
        }

        // GET: FillFields/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FillField fillField = db.Fields.Find(id);
            if (fillField == null)
            {
                return HttpNotFound();
            }
            return View(fillField);
        }

        // POST: FillFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FillField fillField = db.Fields.Find(id);
            db.Fields.Remove(fillField);
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
    }
}
