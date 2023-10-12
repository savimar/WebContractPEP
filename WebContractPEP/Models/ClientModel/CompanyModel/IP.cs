using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebContractPEP.Models.ClientModel.PersonModel;

namespace WebContractPEP.Models.ClientModel.CompanyModel
{
    public class IP : Person
    {
        [Required] public string INNIP { get; set; } //ИНН
        public string OGRNIP { get; set; }//огрнип
    }
}