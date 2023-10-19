using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebContractPEP.Models;
using WebContractPEP.Models.DTO;

namespace WebContractPEP.Controllers
{
    public class ContractsApiController : ApiController
    {
        private ContractContext db = new ContractContext();

        // GET: api/ContractsApi
        public IEnumerable<ContractDto> GetContracts()
        {
           
                List<Contract> contracts= db.Contracts.ToList();
                var dtos = new List<ContractDto>();
                contracts.ForEach(c=> dtos.Add(new ContractDto(c)));
                return dtos;
        }

        // GET: api/ContractsApi/5
        [ResponseType(typeof(Contract))]
        public async Task<IHttpActionResult> GetContract(long id)
        {
            Contract contract = await db.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            //return Ok(contract);
            return new HtmlContractResult(contract);
        }

        // PUT: api/ContractsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContract(long id, Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.ContractId)
            {
                return BadRequest();
            }

            db.Entry(contract).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContractsApi
        [ResponseType(typeof(Contract))]
        public async Task<IHttpActionResult> PostContract(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contracts.Add(contract);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contract.ContractId }, contract);
        }

        // DELETE: api/ContractsApi/5
        [ResponseType(typeof(Contract))]
        public async Task<IHttpActionResult> DeleteContract(long id)
        {
            Contract contract = await db.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            db.Contracts.Remove(contract);
            await db.SaveChangesAsync();

            return Ok(contract);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractExists(long id)
        {
            return db.Contracts.Count(e => e.ContractId == id) > 0;
        }
    }
}