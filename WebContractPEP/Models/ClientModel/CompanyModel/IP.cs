using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebContractPEP.Models.ClientModel.PersonModel;

namespace WebContractPEP.Models.ClientModel.CompanyModel
{
    public class IP : UL
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")][DisplayName("Имя")]/*[UIHint("String")] */public string FirstName { get; set; } //Имя об
        public string MiddleName { get; set; }   //Отчетсво
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")][DisplayName("Фамилия")] public string LastName { get; set; } //Фамилия об
        public string OGRNIP { get; set; }//огрнип
                                          // public virtual Passport Passport { get; set; }  //документ ToDo убрать ошибку https://stackoverflow.com/questions/22610063/schema-specified-is-not-valid-errors-the-relationship-was-not-loaded-because-t
        public string FIO
        {
            get
            {
                string str = FirstName;
                if (string.IsNullOrEmpty(MiddleName))
                    return str + " " + MiddleName + " " + LastName;
                else return str + " " + LastName;

            }

        }
    }
}