﻿using System;
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
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Products
        public IQueryable<OutletProduct> Getoutlet_product()
        {
            return db.outlet_product;
        }

        // GET: api/Products/5
        [ResponseType(typeof(OutletProduct))]
        public async Task<IHttpActionResult> GetOutletProduct(int outlet_id)
        {
            //await db.outlet_product.Join(db.merchant_product,
            //                     outletProduct => outletProduct.merchant_product_id,
            //                     merchantProduct => merchantProduct.merchant_product_id,
            //                     (outletProduct, merchantProduct) => outletProduct).Where(outletProduct.).ToListAsync();
            //OutletProduct outletProduct = await db.outlet_product.FindAsync(outlet_id);
            //if (outletProduct == null)
            //{
            //    return NotFound();
            //}
            //var MessageList = await db.merchant_product
            //.Join(db.outlet_product, u => u.merchant_id, uir => uir.outlet_id,
            //(u, uir) => new { u, uir })
            //.Select(i => new 
            //{
            //    merchant_product_id = i.u.merchant_product_id,
            //    name = i.u.name,
            //    price = i.u.price,
            //    product_image = i.u.product_image,
            //    avg_ratings = i.u.avg_ratings,
            //    outlet_id = i.uir.outlet_id
            //})
            //.Where(m => m.outlet_id == outlet_id)
            //.ToListAsync();
            var results = from mp in db.merchant_product
                          join op in db.outlet_product on mp.merchant_product_id equals op.merchant_product_id
                          where (op.outlet_id == outlet_id && mp.deleted==false)
                          select new MenuBindingModels.GetOutletMenu
                          {
                              merchant_product_id = mp.merchant_product_id,
                              name = mp.name,
                              price = mp.price,
                              product_image = mp.product_image,
                              avg_ratings = mp.avg_ratings,
                              food_type_id = mp.food_type,
                              outlet_id = op.outlet_id
                          };

            var MessageList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(MessageList);
            
        }
        // GET: api/Products/5
        [ResponseType(typeof(OutletProduct))]
        public async Task<IHttpActionResult> GetSingleOutletProduct(int product_id)
        {
            var results = from mp in db.merchant_product
                          join op in db.outlet_product on mp.merchant_product_id equals op.merchant_product_id
                          where (mp.merchant_product_id == product_id && mp.deleted==false)
                          select new MenuBindingModels.GetOutletMenu
                          {
                              merchant_product_id = mp.merchant_product_id,
                              name = mp.name,
                              price = mp.price,
                              product_image = mp.product_image,
                              avg_ratings = mp.avg_ratings,
                              food_type_id = mp.food_type,
                              outlet_id = op.outlet_id,
                              outletproduct_id = op.outlet_product_id
                          };

            var Product = await results.FirstOrDefaultAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(Product);

        }
        // GET: api/SingleProducts/5
        [Route("api/GetSingleProduct")]
        [ResponseType(typeof(MerchantProduct))]
        public async Task<IHttpActionResult> GetSingleProduct(int product_id)
        {
            var results = from mp in db.merchant_product
                          where (mp.merchant_product_id == product_id && mp.deleted==false)
                          select new 
                          {
                              merchant_product_id = mp.merchant_product_id,
                              name = mp.name,
                              price = mp.price,
                              product_image = mp.product_image,
                              avg_ratings = mp.avg_ratings,
                              food_type = mp.food_type
                          };

            var Product = await results.FirstOrDefaultAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(Product);

        }
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOutletProduct(int id, OutletProduct outletProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != outletProduct.outlet_product_id)
            {
                return BadRequest();
            }

            db.Entry(outletProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutletProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(OutletProduct))]
        public async Task<IHttpActionResult> PostOutletProduct(OutletProduct outletProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.outlet_product.Add(outletProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = outletProduct.outlet_product_id }, outletProduct);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(OutletProduct))]
        public async Task<IHttpActionResult> DeleteOutletProduct(int id)
        {
            OutletProduct outletProduct = await db.outlet_product.FindAsync(id);
            if (outletProduct == null)
            {
                return NotFound();
            }

            db.outlet_product.Remove(outletProduct);
            await db.SaveChangesAsync();

            return Ok(outletProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OutletProductExists(int id)
        {
            return db.outlet_product.Count(e => e.outlet_product_id == id) > 0;
        }
    }
}