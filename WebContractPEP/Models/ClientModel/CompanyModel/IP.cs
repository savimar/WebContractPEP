using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebContractPEP.Models.ClientModel.PersonModel;

namespace WebContractPEP.Models.ClientModel.CompanyModel
{
    public class IP : Person
    {
        [Required] public string INN { get; set; } //инн
        public virtual ICollection<BankDetail> BankDetails { get; set; } =
            new List<BankDetail>(); //список банковских реквизитов

        public virtual ICollection<ContractTemplate> ContractTemplates { get; set; } =
            new List<ContractTemplate>(); //список шаблонов договора конкретного пользователя
            public string OGRNIP { get; set; }//огрнип
         public override ICollection<Contract> Contracts { get; set; } = new List<Contract>(); //заключенные контракты
                                                                                              //
        public string IPFIO
        {
            get
            {
                string str = "Индивидуальный предприниматель "+ FirstName;
                if (string.IsNullOrEmpty(MiddleName))
                    return str + " " + MiddleName + " " + LastName;
                else return str + " " + LastName;

            }

        }
    }
}