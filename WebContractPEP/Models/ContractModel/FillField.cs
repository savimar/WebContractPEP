using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebContractPEP.Models.ClientModel;
using System.Web.WebPages;

namespace WebContractPEP.Models
{
    public enum FieldType //
    {
      [Description("Текст")] String,
    [Description("Число")] Number,
    [Description("Дата")] Date,
    [Description("Фото или скан")] Image


    }
    public enum AutoFillFieldType
    {
        [Description("Номер договора")] ContractNumber,
        [Description("Дата договора")] ContractDate,
        [Description("Полные ФИО")] FullFIO, //полное ФИО
        [Description("Номер телефона")] Phone,
        [Description("E-mail")] Email,
        [Description("Серия паспорта")] PassportSeria,
        [Description("Номер паспорта")] PassportNumber,
        [Description("Дата выдачи паспорта")] PassportDate,
        [Description("Кем выдан паспорт")] PassportIssueBy,
        [Description("Код подразделения")] IssueID,
        [Description("Адрес")] Address,
        [Description("Город (для шапки)")] City, //город для шапки договора
        [Description("Фамилилия и инициалы")] Initials, //Фамилия инициалы физика


    }
    public class FillField : ICloneable
    {
       
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? FieldId { get; set; }
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string AutoFieldValue { get; set; }
        public virtual ContractTemplate  ContractTemplate{ get; set; }
        public long ContractTemplateId { get; set; }
        public FieldType FieldType { get; set; }
        public AutoFillFieldType? AutoFillFieldType { get; set; } // поле, заполняемое предопределённым значением
        public bool IsRequired { get; set; } = false;
        public bool IsFilledExecutor { get; set; } = false; // заполненяется исполнителем
        public bool IsFilledClient { get; set; } = false; //заполненяется клиентом
        public bool IsNeedSummInWords { get; set; } = false; //нужна сумма прописью
        public bool? IsAutoFillField { get; set; } = false; //поле для всех клиентов?


        public object Clone()
        {
            return new FillField
            {
                FieldId = null,
                FieldName= this.FieldName,
                AutoFieldValue = this.AutoFieldValue,
                ContractTemplate = null,
                ContractTemplateId = 0,
                FieldType = this.FieldType,
                AutoFillFieldType = this.AutoFillFieldType, 
                IsRequired = this.IsRequired,
                IsFilledExecutor = this.IsFilledExecutor,
                IsFilledClient = this.IsFilledClient,
                IsNeedSummInWords = this.IsNeedSummInWords,
                IsAutoFillField= false
               
            };
        }
    }
}