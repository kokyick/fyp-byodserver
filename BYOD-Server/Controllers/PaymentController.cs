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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PaymentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Payment
        public IQueryable<PaymentHistory> GetPaymentHistories()
        {
            return db.PaymentHistories;
        }

        // GET: api/Payment/5
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> GetPaymentHistory(int id)
        {
            PaymentHistory paymentHistory = await db.PaymentHistories.FindAsync(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            return Ok(paymentHistory);
        }

        // PUT: api/Payment/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPaymentHistory(int id, PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentHistory.paymentHistory_id)
            {
                return BadRequest();
            }

            db.Entry(paymentHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentHistoryExists(id))
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

        // POST: api/Payment
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> PostPaymentHistory(PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentHistories.Add(paymentHistory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = paymentHistory.paymentHistory_id }, paymentHistory);
        }

        // DELETE: api/Payment/5
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> DeletePaymentHistory(int id)
        {
            PaymentHistory paymentHistory = await db.PaymentHistories.FindAsync(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            db.PaymentHistories.Remove(paymentHistory);
            await db.SaveChangesAsync();

            return Ok(paymentHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentHistoryExists(int id)
        {
            return db.PaymentHistories.Count(e => e.paymentHistory_id == id) > 0;
        }
    }
}