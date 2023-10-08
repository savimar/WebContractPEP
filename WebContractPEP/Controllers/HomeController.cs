﻿using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebContractPEP.Models;


namespace WebContractPEP.Controllers
{
    public class HomeController : Controller
    {

        /*   [HttpGet]
           public ActionResult Index()
           {
               return View();
           }
        */
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            ContractTemplate model = new ContractTemplate();
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContractTemplate model)
        {
            return View(model);
        }
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            string folder = "~/Files/";
            if (!Directory.Exists(Server.MapPath(folder)))
            {
                {
                    Directory.CreateDirectory(Server.MapPath(folder));
                }
            }

            ;
            string path = string.Empty;
            object fileSavePath;
            ContractTemplate template = new ContractTemplate();
           
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
                    Sample sample = new Sample();
                    sample.ContractTemplate = template;
                    sample.Name = "samle" + i;
                    sample.Text = document.Words[i].Text;
                    sampleList.Add(sample);

                }
                template.Samples = sampleList;
                template.Name = fileName;


                document.Close();
                //Delete the Uploaded Word File.

                // ViewBag.template = template;
                System.IO.File.Delete(fileSavePath.ToString());
            }
            else
            {
                return RedirectToAction("View", "ContractTemplates", ViewBag); // нет файла для загрузки, обработать //ToDo
            }
            db.ContractTemplates.Add(template);
            db.SaveChanges();
            long id = template.ContactTemplateId;
            ViewData["id"] = id;
            ViewBag.id = id;
            TempData["id"] = id;
            HttpContext.Session["id"] = id;
            ViewBag.message = id;
            return RedirectToAction("Details", "ContractTemplates", new { id });
        }

    }
}