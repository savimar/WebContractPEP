using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebContractPEP.Models.ClientModel
{
    public class BankDetail
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BankDetailId { get; set; }

        public bool IsActive { get; set; } = true;
        [Required] public string BankName { get; set; }
        [Required] public string BankCorrespondentAccount { get; set; } //корреспонденский счет
        [Required] public string NumberAccount { get; set; }// счет юридического лица
        public bool BankStatus { get; set; } = true;  //статус счета действющий true/недействующий  false         
        [Required] public string BankId { get; set; } //БИК
        public virtual Client Client { get; set; }

    }
}
}