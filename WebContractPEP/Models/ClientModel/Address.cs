using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace WebContractPEP.Models.ClientModel
{
    public enum AddressType //
    {
        Registration,//прописка
        Legal,// юридический адрес
        Actual,//фактический
        Residential//адрес проживания
    }
    public class Address
    {
        [AllowHtml]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressId { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Client Client { get; set; }
        public virtual long? ClientId { get; set; }
        [DisplayName("Город")] public string City { get; set; }      //город  
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string Region { get; set; }      // область, регион
        [DisplayName("Почтовый индекс")] public string ZipCode { get; set; } //индекс
        public string Country { get; set; } //страна
        public string CountryId { get; set; } = "643"; //код страны Россия
        public string Area { get; set; } //район
        public string Settlement { get; set; } //населенный пункт
        public string District { get; set; } //округ
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string Street { get; set; } //улица
        [Required(ErrorMessage = "Поле обязательно для заполнения")] public string House { get; set; } //дом
        public string Frame { get; set; } //корпус
        public string Building { get; set; } //строение
        public string Flat { get; set; } //квартира
        public string Room { get; set; } //помещение
        public string FiasCode { get; set; } //код ФИАС
        public AddressType AddressType { get; set; } // юридический, прописки, проживания, местоположения. фактический

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Адрес")]
        public string FullAddress
        {
            get
            {
                return GetFullAddress(String.Empty);
            }
        }
        private string GetFullAddress(string str)
        {
            var comma = ", ";
            if (!ZipCode.IsNullOrWhiteSpace()) str += ZipCode + comma;
            //str += CountryId + ; //код 	<countryId> – идентификатор страны Россия

            str += Region + comma;
            if (!Area.IsNullOrWhiteSpace()) str += "район " + Area + comma;
            if (!City.IsNullOrWhiteSpace()) str += "г. " + City + comma;
            if (!Settlement.IsNullOrWhiteSpace()) str += Settlement + comma;
            if (!District.IsNullOrWhiteSpace()) str += District + comma;
            str += Street + comma;
            str += "д." + House + comma;
            if (!Flat.IsNullOrWhiteSpace()) str += "кв. " + Flat + comma;
            if (!Room.IsNullOrWhiteSpace()) str += Room + ".";

            return str;

        }
    }
}
