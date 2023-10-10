using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Office.Interop.Word;
using WebContractPEP.Models;


namespace WebContractPEP.Controllers
{
    public class HomeController : Controller
    {
        private ContractContext db = new ContractContext();

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