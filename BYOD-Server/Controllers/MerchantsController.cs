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
using BYOD_Server.Models;
using System.Web.Http.Cors;

namespace BYOD_Server.Controllers
{
    [RoutePrefix("api/Merchant")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MerchantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Merchants
        [Route("getMerchant")]
        public IQueryable<Merchants> GetMerchants()
        {
            return db.Merchants;
        }

        // GET: api/Merchants/5
        [Route("api/getMerchant")]
        [ResponseType(typeof(Merchants))]
        public async Task<IHttpActionResult> GetMerchants(int id)
        {
            Merchants merchants = await db.Merchants.FindAsync(id);
            if (merchants == null)
            {
                return NotFound();
            }

            return Ok(merchants);
        }

        // PUT: api/Merchants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMerchants(int id, Merchants merchants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != merchants.merchant_id)
            {
                return BadRequest();
            }

            db.Entry(merchants).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantsExists(id))
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

        // POST: api/Merchants
        [ResponseType(typeof(Merchants))]
        public async Task<IHttpActionResult> PostMerchants(Merchants merchants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Merchants.Add(merchants);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = merchants.merchant_id }, merchants);
        }

        // DELETE: api/Merchants/5
        [ResponseType(typeof(Merchants))]
        public async Task<IHttpActionResult> DeleteMerchants(int id)
        {
            Merchants merchants = await db.Merchants.FindAsync(id);
            if (merchants == null)
            {
                return NotFound();
            }

            db.Merchants.Remove(merchants);
            await db.SaveChangesAsync();

            return Ok(merchants);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerchantsExists(int id)
        {
            return db.Merchants.Count(e => e.merchant_id == id) > 0;
        }
    }
}