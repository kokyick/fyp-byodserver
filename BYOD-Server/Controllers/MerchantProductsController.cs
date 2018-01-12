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
    public class MerchantProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MerchantProducts
        public IQueryable<MerchantProduct> Getmerchant_product()
        {
            return db.merchant_product;
        }


        // GET: api/MerchantProducts/5
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> GetMerchantProduct(int id)
        {
            MerchantProduct merchantProduct = await db.merchant_product.FindAsync(id);
            if (merchantProduct == null)
            {
                return NotFound();
            }

            return Ok(merchantProduct);
        }
        // GET: api/GetMerchantProducts/5
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> GetMerchantProducts(int merchant_id)
        {
            var merchantProduct = await db.merchant_product.Where(m => m.merchant_id == merchant_id && m.deleted != true).ToListAsync();
            if (merchantProduct == null)
            {
                return NotFound();
            }

            return Ok(merchantProduct);
        }

        // PUT: api/MerchantProducts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMerchantProduct(int id, MerchantProduct merchantProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != merchantProduct.merchant_product_id)
            {
                return BadRequest();
            }

            db.Entry(merchantProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantProductExists(id))
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

        // POST: api/MerchantProducts
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> PostMerchantProduct(MerchantProduct merchantProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.merchant_product.Add(merchantProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = merchantProduct.merchant_product_id }, merchantProduct);
        }

        // PostSoftDELETE: api/MerchantProducts/5
        [Route("api/SoftDeleteMerchantProduct")]
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> PostSoftDeleteMerchantProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //MerchantProduct product = await db.merchant_product.FindAsync(id);
            MerchantProduct product = await db.merchant_product
              .Where(x => (x.merchant_product_id.Equals(id))).SingleOrDefaultAsync();
            if (product == null)
            {
                return Ok("Invaild Request");
            }
            product.deleted = true;
            await db.SaveChangesAsync();

            return Ok(product);
        }
        // DeleteOutletProduct - remove dishes from outlet
        [Route("api/DeleteOutletProduct")]
        public async Task<IHttpActionResult> PostDeleteOutletProduct(OutletProduct outletProd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //OutletProduct outletProducts = await db.outlet_product.FindAsync(outletProd.outlet_product_id);
            OutletProduct outletProducts = await db.outlet_product
              .Where(x => (x.merchant_product_id==(outletProd.merchant_product_id) 
              && x.outlet_id == (outletProd.outlet_id))).SingleOrDefaultAsync();
            //MerchantProduct product = await db.merchant_product.FindAsync(id);
            if (outletProducts == null)
            {
                return Ok("Invaild Request");
            }
            db.outlet_product.Remove(outletProducts);

            await db.SaveChangesAsync();

            return Ok("deleted from outlet");
        }
        // DELETE: api/MerchantProducts/5
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> DeleteMerchantProduct(int id)
        {
            //delete from 
            MerchantProduct merchantProduct = await db.merchant_product.FindAsync(id);
            if (merchantProduct == null)
            {
                return NotFound();
            }

            db.merchant_product.Remove(merchantProduct);
            await db.SaveChangesAsync();

            return Ok(merchantProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MerchantProductExists(int id)
        {
            return db.merchant_product.Count(e => e.merchant_product_id == id) > 0;
        }
    }
}