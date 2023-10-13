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

            var persons = new List<Person>
            {

                new Person
                {
                    FirstName = "Тест", LastName = "Тестов", BirthDate = DateTime.Parse("1980/01/02"),
                    ClientType = ClientType.Person, Email = "123@74.ru", Phone = "+79111111111", Id = 1
                },
                
            };
            var addresses = new List<Address>
            {
                new Address { Street = "Тестовая улица", House = "4а", Region = "тестовый город"}
                
            };
            var companies = new List<Company>
            {
                new Company
                {
                    Name = "ООО Тест", INN = "5555555555", KPP = "1111111", ClientType = ClientType.Company,
                    Email = "823@test.ru", Phone = "+79121111111", Id = 2

                },
                
            };
           
            
            var text = "ООО Тест, действующее на основании Устава и именуемое в дальнейшем “исполнитель”, и Тестова Теста Тестовна, именуемое в дальнейшем “Заказчик”, заключили настоящий договор о нижеследующем.";

           





            List<Client> clients = new List<Client>() {  new Person
                {
                    FirstName = "Тест", LastName = "Тестов", BirthDate = DateTime.Parse("1980/01/02"),
                    ClientType = ClientType.Person, Email = "123@74.ru", Phone = "+79111111111"
                }, new Company
                {
                    Name = "ООО Тест", INN = "5555555555", KPP = "1111111", ClientType = ClientType.Company,
                    Email = "823@test.ru", Phone = "+79121111111"

                } };

            List<ContractTemplate> templates = new List<ContractTemplate>
            {
                new ContractTemplate{ Name = "часть1", Client = clients.Last(), FinalText = text},
               
            };
            var fields = new List<FillField>
            {

                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FillFieldId = 1, FieldName = "Фамилия Имя Отчество клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.FullFIO, IsFilledExecutor = true
                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FillFieldId = 2, FieldName = "Телефон клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.Phone, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FillFieldId = 3, FieldName = "E-mail клиента",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.Email, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FillFieldId = 4, FieldName = "Серия паспорта",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.PassportSeria, IsFilledExecutor = true

                },
                new FillField
                {
                    ContractTemplate = templates.FirstOrDefault(), FillFieldId = 5, FieldName = "Номер паспорта",
                    FieldType = FieldType.String, IsAutoFillField = true, AutoFillFieldType = AutoFillFieldType.PassportNumber, IsFilledExecutor = true
                },




            };

            List<Contract> contracts = new List<Contract>
            {
                new Contract
                {
                    ContractNumber = "1", Concluding = clients, ContractDate = DateTime.Now,
                  
                   }

            };
           
            contracts.ForEach(s => db.Contracts.Add(s)); 
            templates.ForEach(t => db.Templates.Add(t));
            fields.ForEach(t => db.Fields.Add(t));
            db.SaveChanges();
        }
    }
}
