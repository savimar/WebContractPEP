using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using WebContractPEP.Models.ContractModel;

namespace WebContractPEP.Models.ClientModel
{
    public enum ClientType //тип клиента
    {
        Person,
        IP,
        Company
    }
    public class Client
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
        public bool IsActive { get; set; } = true;
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }   //Номер телефона для ПЭП
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();// юридический адрес, адрес прописки, адрес проживания
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>(); //заключенные контранты
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public ClientType ClientType { get; set; }
        public virtual ICollection<ContractTemplate> Templates { get; set; } = new List<ContractTemplate>();// шаблоны договоров
        public virtual ICollection<BankDetail> BankDetails { get; set; } = new List<BankDetail>(); //список банковских реквизитов
        public virtual ICollection<FillField> Fields { get; set; } = new List<FillField>(); //список кастомных полей этого пользовтаеля
        public virtual ICollection<ContractTemplate> ContractTemplates { get; set; } = new List<ContractTemplate> (); //список шаблонов договора конкретного пользователя
}