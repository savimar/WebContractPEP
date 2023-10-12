using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models.ClientModel.PersonModel
{
    public class OtherDocument : Passport
    {
        public DateTime ValidUntil { get; set; } //действителен до

    }
}