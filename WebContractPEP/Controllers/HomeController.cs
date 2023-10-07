using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}