using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebContractPEP.Models.ClientModel.PersonModel
{
    public class Person : Client
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")][DisplayName("Имя")]/*[UIHint("String")] */public string FirstName { get; set; } //Имя об
        public string MiddleName { get; set; }   //Отчетсво
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")][DisplayName("Фамилия")] public string LastName { get; set; } //Фамилия об
        [Required(ErrorMessage = "Дата рождения обязательна для заполнения")]
        [DataType(DataType.Date)][DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)][DisplayName("Дата рождения")] public DateTime? BirthDate { get; set; }   //Дата рождения 
        public string SNILS { get; set; } //снилс
        public bool IsResidentialAddressEqualRegistrationAddress { get; set; } = false; //Адрес регистрации совпадает с адрем проживания
        public virtual ICollection<Passport> Passports { get; set; } = new List<Passport>(); //документ

        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        public string FullName
        {

            get
            {
                string str = LastName + " " + FirstName;
                if (string.IsNullOrEmpty(MiddleName))
                    return str;
                else return str + " " + MiddleName;
            }
        }
        public string Inicials
        {
            get
            {
                string str = LastName + " " + FirstName.ToUpperInvariant()[0] + ". ";
                if (string.IsNullOrEmpty(MiddleName)) return str;
                else return str + MiddleName?.ToUpperInvariant()[0] + ".";
            }
        }
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