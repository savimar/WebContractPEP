using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models
{
    public class ContractTemplateDTO
    {
        public ContractTemplate template { get; set; }
        public IEnumerable<FillField> ListFillFields { get; set; }
    }
}