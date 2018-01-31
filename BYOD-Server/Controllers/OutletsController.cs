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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace BYOD_Server.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OutletsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/Outlets
        public async Task<IHttpActionResult> GetOutlets()
        {
            var outlets = await db.Outlets.ToListAsync();
            if (outlets == null)
            {
                return NotFound();
            }

            return Ok(outlets);
        }
        // GET: api/Outlets/
        
        [Route("api/GetMerchantOutlets")]
        [ResponseType(typeof(Outlets))]
        public async Task<IHttpActionResult> GetMerchantOutlets()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var outlets = from o in db.Outlets
                          join merchant in db.Merchants on o.merchant_id equals merchant.merchant_id
                          join u in db.Users on merchant.user_id equals u.Id
                          where u.Id == user.Id
                          select new MenuBindingModels.MOutlet
                          {
                              avg_ratings = o.avg_ratings,
                              closing_time = o.closing_time,
                              gst = o.gst,
                              merchant_id = o.merchant_id,
                              contact_no = o.contact_no,
                              featured_photo = o.featured_photo,
                              last_review_time = o.last_review_time,
                              lat = o.lat,
                              lon = o.lon,
                              name = o.name,
                              opening_status = o.opening_status,
                              opening_time = o.opening_time,
                              outlet_id = o.outlet_id,
                              postal_code = o.postal_code,
                              servicecharge = o.servicecharge,
                              streetname = o.streetname,
                              total_comments = o.total_comments,
                              unit_no = o.unit_no,
                          };
            var outletList = await outlets.ToListAsync();
            return Ok(outletList);
        }

        // GET: api/Outlets/5
        [ResponseType(typeof(Outlets))]
        public async Task<IHttpActionResult> GetOutlets(int id)
        {
            Outlets outlets = await db.Outlets.FindAsync(id);
            if (outlets == null)
            {
                return NotFound();
            }

            return Ok(outlets);
        }
        // GET: api/Outlets?outlet_id=5
        [ResponseType(typeof(Outlets))]
        public async Task<IHttpActionResult> GetOutletDetails(int outlet_id)
        {
            var outlet = await db.Outlets.Where(m => m.outlet_id == outlet_id).SingleAsync();
            if (outlet == null)
            {
                return NotFound();
            }

            return Ok(outlet);
        }

        // PUT: api/Outlets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOutlets(int id, Outlets outlets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != outlets.outlet_id)
            {
                return BadRequest();
            }
            outlets.opening_status = true;
            db.Entry(outlets).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutletsExists(id))
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

        // POST: api/Outlets
        [ResponseType(typeof(Outlets))]
        public async Task<IHttpActionResult> PostOutlets(Outlets outlets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Outlets.Add(outlets);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = outlets.outlet_id }, outlets);
        }

        // DELETE: api/Outlets/5
        [ResponseType(typeof(Outlets))]
        public async Task<IHttpActionResult> DeleteOutlets(int id)
        {
            Outlets outlets = await db.Outlets.FindAsync(id);
            if (outlets == null)
            {
                return NotFound();
            }

            db.Outlets.Remove(outlets);
            await db.SaveChangesAsync();

            return Ok(outlets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OutletsExists(int id)
        {
            return db.Outlets.Count(e => e.outlet_id == id) > 0;
        }
    }
}