using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


namespace WebContractPEP.Models.ClientModel
{
   

    public class Client
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsActive { get; set; } = true;
        [DataType(DataType.PhoneNumber)] public string Phone { get; set; } //Номер телефона для ПЭП
        [DataType(DataType.EmailAddress)] public string Email { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } =
            new List<Address>(); // юридический адрес, адрес прописки, адрес проживания

     

        
    }
}