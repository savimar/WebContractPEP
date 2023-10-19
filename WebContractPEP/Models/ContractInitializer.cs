using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebContractPEP.Models.ClientModel;
using WebContractPEP.Models.ClientModel.CompanyModel;
using WebContractPEP.Models.ClientModel.PersonModel;

namespace WebContractPEP.Models
{
    public class ContractInitializer : DropCreateDatabaseAlways<ContractContext>
    {
        protected override void Seed(ContractContext db)
        {

           
            var addresses = new List<Address>
            {
                new Address { Street = "Тестовая улица", House = "4а", Region = "тестовый город"}
                
            };
            var persons = new List<Person>
            {

                new Person
                {
                    FirstName = "Тест", LastName = "Тестов", BirthDate = DateTime.Parse("1980/01/02"),
                    Email = "123@74.ru", Phone = "+79111111111", Id = 1, Addresses = addresses
                },

            };
            var companies = new List<Company>
            {
                new Company
                {
                    Name = "ООО Тест", INN = "5555555555", KPP = "1111111",
                    Email = "823@test.ru", Phone = "+79121111111", Id = 2

                },
                
            };
           
            
            var text = "ООО Тест, действующее на основании Устава и именуемое в дальнейшем “исполнитель”, и Тестова Теста Тестовна, именуемое в дальнейшем “Заказчик”, заключили настоящий договор о нижеследующем.";

           
            
            List<ContractTemplate> templates = new List<ContractTemplate>
            {
                new ContractTemplate{ Name = "часть1", Company = companies.FirstOrDefault(), FinalText = text},
               
            };
            var fields = new List<FillField>
            {

                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FieldId = 1, FieldName = "Фамилия Имя Отчество клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.FullFIO, IsFilledExecutor = true
                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(),  FieldId = 2, FieldName = "Телефон клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.Phone, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FieldId = 3, FieldName = "E-mail клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.Email, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FieldId = 4, FieldName = "Серия паспорта",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.PassportSeria, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FieldId = 5, FieldName = "Номер паспорта",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.PassportNumber, IsFilledExecutor = true
                },




            };

            List<Contract> contracts = new List<Contract>
            {
                new Contract
                {
                    ContractNumber = "1", ClientPerson = persons.FirstOrDefault(), Company = companies.FirstOrDefault(),ContractDate = DateTime.Now, ContractText = text
                  
                   }

            };
            persons.ForEach(p=>db.Persons.Add(p));
            companies.ForEach(c=>db.Companies.Add(c));
            contracts.ForEach(s => db.Contracts.Add(s)); 
            templates.ForEach(t => db.Templates.Add(t));
            fields.ForEach(t => db.Fields.Add(t));
            db.SaveChanges();
        }
    }
}
