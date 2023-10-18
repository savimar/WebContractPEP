using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebContractPEP.Models.ClientModel;
using WebContractPEP.Models.ClientModel.CompanyModel;


namespace WebContractPEP.Models
{
    public class ContractTemplate
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContractTemplateId { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public virtual UL Client { get; set; }
        public string FinalText { get; set; }
        public virtual ICollection<FillField> Fields { get; set; } =
            new List<FillField>(); //список полей шаблона
    }
}