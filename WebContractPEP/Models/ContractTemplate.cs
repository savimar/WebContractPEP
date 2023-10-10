using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebContractPEP.Models
{
    public class ContractTemplate
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContactTemplateId { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public string FinalText { get; set; }
    }
}