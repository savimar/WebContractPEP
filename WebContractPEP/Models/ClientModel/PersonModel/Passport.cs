using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebContractPEP.Models.ClientModel.PersonModel
{
    public enum CitienshipType //гражданство
    {
        RF,//РФ
        SNG, //СНГ
        AnotherCountries, //Другие страны
        NoCitienship, //без гражданства
        ResidentCard,//вид на жительство
        OtherDocument //другие документы
    }
    public class Passport
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PassportId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime IssueDate { get; set; } //когда выдан
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string Number { get; set; } = string.Empty; //номер
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public CitienshipType CitienshipType { get; set; }
        public Client Client { get; set; }
    }
}
