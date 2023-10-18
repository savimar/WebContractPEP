using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models.ClientModel.CompanyModel
{
    public enum ClientType //тип клиента
    {
        IP,
        Company
    }
    public class UL: Client
    {
        [Required] public string INN { get; set; } //инн
        public virtual ICollection<BankDetail> BankDetails { get; set; } =
            new List<BankDetail>(); //список банковских реквизитов
        
        public virtual ICollection<ContractTemplate> ContractTemplates { get; set; } =
            new List<ContractTemplate>(); //список шаблонов договора конкретного пользователя
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public ClientType ClientType { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>(); //заключенные контранты
    }
}
    