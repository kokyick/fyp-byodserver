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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PromocodesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Promocodes
        public IQueryable<Promocodes> GetPromocodes()
        {
            return db.Promocodes;
        }

        // GET: api/Promocodes/5
        [ResponseType(typeof(Promocodes))]
        public async Task<IHttpActionResult> GetPromocodes(int id)
        {
            Promocodes promocodes = await db.Promocodes.FindAsync(id);
            if (promocodes == null)
            {
                return NotFound();
            }

            return Ok(promocodes);
        }
        // GET: api/Promocodes/5
        [ResponseType(typeof(Promocodes))]
        public async Task<IHttpActionResult> GetCheckPromocodes(string promoname)
        {
            Promocodes promocodes = await db.Promocodes.Where(x=>x.promocode_name== promoname).SingleOrDefaultAsync();

            return Ok(promocodes);
        }

        // PUT: api/Promocodes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPromocodes(int id, Promocodes promocodes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promocodes.promocodes_id)
            {
                return BadRequest();
            }

            db.Entry(promocodes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocodesExists(id))
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

        // POST: api/Promocodes
        [ResponseType(typeof(Promocodes))]
        public async Task<IHttpActionResult> PostPromocodes(Promocodes promocodes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Promocodes.Add(promocodes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = promocodes.promocodes_id }, promocodes);
        }

        // DELETE: api/Promocodes/5
        [ResponseType(typeof(Promocodes))]
        public async Task<IHttpActionResult> DeletePromocodes(int id)
        {
            Promocodes promocodes = await db.Promocodes.FindAsync(id);
            if (promocodes == null)
            {
                return NotFound();
            }

            db.Promocodes.Remove(promocodes);
            await db.SaveChangesAsync();

            return Ok(promocodes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromocodesExists(int id)
        {
            return db.Promocodes.Count(e => e.promocodes_id == id) > 0;
        }
    }
}