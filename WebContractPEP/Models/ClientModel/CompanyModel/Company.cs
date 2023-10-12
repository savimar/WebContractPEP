using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models.ClientModel.CompanyModel
{
    public class Company : Client
    {
        [Required] public string Name { get; set; }    //сокращенное наименование компании
        public string FullName { get; set; }    //полное наименование компании
        [Required] public string INN { get; set; } //инн
        public string OGRN { get; set; }//огрн
        public string OKPO { get; set; }//окпо
        [Required] public string KPP { get; set; } //кпп       
        /*[ForeignKey("BankDetailId")] */
        public string CEO { get; set; } //генеральный директор
        public string CEOOf { get; set; } //генеральный директор в родительном падеже
        /* [Column(TypeName = "image")] */
        public byte[] Print { get; set; } //изображение печати организации

    }
}