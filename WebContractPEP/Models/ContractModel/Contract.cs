﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebContractPEP.Models.ClientModel;

namespace WebContractPEP.Models
{
    public class Contract
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContractId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string ContractNumber { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ContractDate { get; set; }
        public byte[] ContractData { get; set; } //готовый в pdf с подписями
        public virtual ICollection<Client> Concluding { get; set; } = new List<Client>(); //кто заключил договор
        public string ContractTexts { get; set; }
        public bool IsSigned { get; set; } = false;
    }
}