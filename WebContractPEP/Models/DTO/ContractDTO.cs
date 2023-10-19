using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using WebContractPEP.Models.ClientModel.CompanyModel;

namespace WebContractPEP.Models
{
    public class ContractDto
    {
        public ContractDto(Contract contract)
        {
            this.contract = contract;
        }

        private Contract contract;

        public string Name
        {
            get
            {
               return contract.Name;
            }

            set => throw new NotImplementedException();
        }
        public string Id
        {
            get
            {
                return contract.ContractId.ToString();
            }

            set => throw new NotImplementedException();
        }
        public string IsActive
        {
            get
            {
                if (contract.IsActive == false)
                {
                    return "Не активный";
                }
                else
                {
                    return "Активный";
                }

            }

            set => throw new NotImplementedException();
        }
        public string ContractNumber
        {
            get
            {
                return contract.ContractNumber;
            }

            set => throw new NotImplementedException();
        }
        public string ContractDate
        {
            get
            {
                return contract.ContractDate.ToString("dd.MM.yyyy");
            }

            set => throw new NotImplementedException();
        }
        public string ContractText
        {
            get
            {
                return contract.ContractText;
            }

            set => throw new NotImplementedException();
        }
        public string Person
        {
            get
            {
                return contract.ClientPerson.FIO;
            }

            set => throw new NotImplementedException();
        }
        public string Executor
        {
            get
            {
                if (contract.IP != null)
                    return contract.IP.IPFIO;
                else return contract.Company.Name;
            }
          set => throw new NotImplementedException();
        }
    }
}