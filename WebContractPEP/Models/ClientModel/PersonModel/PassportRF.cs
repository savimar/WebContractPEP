using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models.ClientModel.PersonModel
{
    public class PassportRF : Passport
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")][StringLength(5)] public string Seria { get; set; } //серия
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string IssueBy { get; set; } // кем выдан
        public string IssueID { get; set; } //код подразделения

    }
}