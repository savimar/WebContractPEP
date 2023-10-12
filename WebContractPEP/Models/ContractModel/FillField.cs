using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebContractPEP.Models.ClientModel;

namespace WebContractPEP.Models
{
    public enum FieldType //
    {
        String,
        Number

    }
    public enum AutoFillFieldType
    {
        ContractNumber,
        ContractDate,
        FullFIO, //полное ФИО
        Phone,
        Email,
        PassportSeria,
        PassportNumber,
        PassportDate,
        PassportIssueBy,
        Address,
        City, //город для шапки договора
        Initials, //Фамилия инициалы физика

    }
    public class FillField
    {
       
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FillFieldId { get; set; }
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string FieldName { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string FieldValue { get; set; }
        public virtual Client Client { get; set; }
        public FieldType FieldType { get; set; }
        public AutoFillFieldType? AutoFillFieldType { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsFilledExecutor { get; set; } = false; // заполненяется исполнителем
        public bool IsFilledClient { get; set; } = false; //заполненяется клиентом
        public bool IsNeedSummInWords { get; set; } = false; //нужна сумма прописью
        public bool? IsAutoFillField { get; set; } = false;




    }
}