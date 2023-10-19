using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebContractPEP.Models.DTO
{
    public class HtmlContractResult : IHttpActionResult
    {
        private Contract contract;

        public HtmlContractResult(Contract contract)
        {
            this.contract= contract;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
           string htmlContract = "<html><head><meta charset=utf-8 /></head></body>" + //ToDo сделать HTML для клиента физика
                                 "<h1>" + contract.Name + "</h1><p>" + contract.Company + "</p><p>"
                                 + contract.ContractText + "</p>" + "</body></html>";
           var response = new HttpResponseMessage();
           response.Content = new StringContent(htmlContract);
           response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

           return Task.FromResult(response);
        }
    }
}