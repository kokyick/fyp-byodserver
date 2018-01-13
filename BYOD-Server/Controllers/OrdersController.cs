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
using System.Data.Entity.Core.Objects;

namespace BYOD_Server.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/GetOpenOrders/5
        [ResponseType(typeof(MenuBindingModels.OpenCloseOrder))]
        [Route("api/OpenOrders")]
        public async Task<IHttpActionResult> GetOpenOrders()
        {
            var results = from o in db.Orders
                          join fo in db.food_ordered on o.order_id equals fo.order_id
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id
                          join m in db.Merchants on mp.merchant_id equals m.merchant_id
                          where o.completed==false
                          select new MenuBindingModels.OpenCloseOrder
                          {
                              name = mp.name,
                              merchant_product_id = mp.merchant_product_id,
                              product_image = mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id = mp.merchant_id,
                              quantity = fo.quantity,
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_time = o.order_time,
                              table_id = o.table_id,
                              order_status = o.completed,
                              food_order_id = fo.food_ordered_id,
                              food_comments=fo.comments,
                              order_comment=o.comments,
                              merchant_name = m.biz_name
                          } into t1
                          group t1 by t1.order_id into g
                          select g.ToList();

            var OrderedFoodList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }
        // GET: api/GetOpenOrders/5
        [ResponseType(typeof(MenuBindingModels.OpenCloseOrder))]
        [Route("api/ClosedOrders")]
        public async Task<IHttpActionResult> GetClosedOrders()
        {
            var results = from o in db.Orders
                          join fo in db.food_ordered on o.order_id equals fo.order_id
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id
                          join m in db.Merchants on mp.merchant_id equals m.merchant_id
                          where o.completed == true
                          select new MenuBindingModels.OpenCloseOrder
                          {
                              name = mp.name,
                              merchant_product_id = mp.merchant_product_id,
                              product_image = mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id = mp.merchant_id,
                              quantity = fo.quantity,
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_time = o.order_time,
                              table_id = o.table_id,
                              order_status = o.completed,
                              food_order_id = fo.food_ordered_id,
                              order_comment = o.comments,
                              food_comments = fo.comments,
                              merchant_name=m.biz_name
                          } into t1
                          group t1 by t1.order_id into g
                          select g.ToList();

            var OrderedFoodList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }
        // GET: api/GetAllOrders/
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        [Route("api/AllOrders")]
        public async Task<IHttpActionResult> GetAllOrders()
        {
            var results = from o in db.Orders
                          join fo in db.food_ordered on o.order_id equals fo.order_id
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id

                          select new MenuBindingModels.GetOrderedItems
                          {
                              name = mp.name,
                              merchant_product_id = mp.merchant_product_id,
                              product_image = mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id = mp.merchant_id,
                              quantity = fo.quantity,
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_time = o.order_time,
                              table_id = o.table_id,
                              order_status = o.completed,
                              food_order_id = fo.food_ordered_id,
                              order_comment = o.comments,
                              food_comments = fo.comments
                          } into t1
                          group t1 by t1.order_id into g
                          select g.ToList();
                                 //{
                                 //    name = .name,
                                 //    merchant_product_id = g.merchant_product_id,
                                 //    product_image = g.FirstOrDefault().product_image,
                                 //    dish_completed = g.FirstOrDefault().dish_completed,
                                 //    price = g.FirstOrDefault().price,
                                 //    merchant_id = g.FirstOrDefault().merchant_id,
                                 //    quantity = g.FirstOrDefault().quantity,
                                 //    order_id = g.FirstOrDefault().order_id,
                                 //    order_bill = g.FirstOrDefault().order_bill,
                                 //    order_status = g.FirstOrDefault().order_status
                                 //};

            var OrderedFoodList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }
        // GET: api/GetOutletOrders/
        [Route("api/GetOutletOrders")]
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        public async Task<IHttpActionResult> GetOutletOrders(int outletID)
        {
            var results = from o in db.Orders
                          join rt in db.RestaurantTable on o.table_id equals rt.table_id
                          join ol in db.Outlets on rt.outlet_id equals ol.outlet_id
                          where ol.outlet_id == outletID
                          select new MenuBindingModels.GetOrder
                          {
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_time=o.order_time,
                              order_status = o.completed,
                              outlet_id=ol.outlet_id,
                              outlet_name=ol.name,
                              order_payment=o.paid
                          };

            var OrdersList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrdersList);
        }
        // GET: api/OrderFoods/
        [Route("api/OrderFoods")]
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        public async Task<IHttpActionResult> GetOrdersFood(int orderid)
        {
            var results = from fo in db.food_ordered
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id
                          join o in db.Orders on fo.order_id equals o.order_id
                          where o.order_id== orderid
                          select new MenuBindingModels.GetOrderedItems
                          {
                              name = mp.name,
                              order_time=o.order_time,
                              merchant_product_id = mp.merchant_product_id,
                              product_image = mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id = mp.merchant_id,
                              quantity = fo.quantity,
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_status = o.completed,
                              order_comment = o.comments,
                              food_comments = fo.comments
                          };

            var OrderedFoodList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }
        // GET: api/GetAllOutOrders/5
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        public async Task<IHttpActionResult> GetAllOutletOrders(int outletId)
        {
            var results = from fo in db.food_ordered
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id
                          join o in db.Orders on fo.order_id equals o.order_id
                          where (op.outlet_id == outletId)
                          select new MenuBindingModels.GetOrderedItems
                          {
                              name = mp.name,
                              merchant_product_id = mp.merchant_product_id,
                              product_image = mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id = mp.merchant_id,
                              quantity = fo.quantity,
                              order_id = o.order_id,
                              order_bill=o.total_bill,
                              order_status=o.completed,
                              order_comment = o.comments,
                              food_comments = fo.comments
                          };

            var OrderedFoodList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }
        // GET: api/Orders/5
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        public async Task<IHttpActionResult> GetOneOrdersDetails(int id)
        {
            var results = from fo in db.food_ordered
                          join op in db.outlet_product on fo.outlet_product_id equals op.outlet_product_id
                          join mp in db.merchant_product on op.merchant_product_id equals mp.merchant_product_id
                          join o in db.Orders on fo.order_id equals o.order_id
                          where (fo.order_id == id)
                          select new MenuBindingModels.GetOrderedItems
                          {
                              name = mp.name,
                              merchant_product_id = mp.merchant_product_id,
                              product_image=mp.product_image,
                              dish_completed = fo.served,
                              price = mp.price,
                              merchant_id=mp.merchant_id,
                              quantity=fo.quantity,
                              order_id=o.order_id,
                              order_bill = o.total_bill,
                              order_status = o.completed,
                              order_comment = o.comments,
                              food_comments = fo.comments
                          };

            var OrderedFoodList = await results.FirstOrDefaultAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrderedFoodList);
        }

        //START: edit order

        // POST: api/CancelOrder
        [Route("api/CancelOrder")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostCancelOrder(int orderId)
        {
            var results = from foodordered in db.food_ordered
                          where (orderId == foodordered.order_id)
                          select new 
                          {
                              foodId = foodordered.food_ordered_id
                          };

            var OrderedFoodList = await results.ToListAsync();
            foreach (var i in OrderedFoodList)
            {
                FoodOrdered food = db.food_ordered.Find(i.foodId);
                if (food != null)
                {
                    //remove food in order
                    db.food_ordered.Remove(food);
                    db.SaveChanges();
                }
            }
            //remove order
            Orders getorder = db.Orders.Find(orderId);
            if (getorder != null)
            {
                db.Orders.Remove(getorder);
                await db.SaveChangesAsync();
                return Ok("Removed");
            }
            else
            {
                return Ok("Orders not found");
            }
        }
        // POST: api/EditOrder
        [Route("api/EditOrder")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostEditOrder(Orders updated_order)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Orders getorder = db.Orders.Find(updated_order.order_id);
            if (getorder != null)
            {
                getorder = updated_order;
                await db.SaveChangesAsync();
                return Ok(getorder);
            }
            else
            {
                return Ok("Order not found");
            }
        }
        // POST: api/RemoveOrderFood
        [Route("api/RemoveOrderFood")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostRemoveOrderFood(int foodOrderedID)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FoodOrdered getfood = db.food_ordered.Find(foodOrderedID);
            if (getfood != null)
            {
                db.food_ordered.Remove(getfood);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return Ok("404 food not found");
            }
        }

        // POST: api/addFoodOrder
        [Route("api/addFoodOrder")]
        [ResponseType(typeof(FoodOrdered))]
        public async Task<IHttpActionResult> PostAddFoodOrder(FoodOrdered food)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.food_ordered.Add(food);
            await db.SaveChangesAsync();

            return Ok(food);
        }
        //END: edit order

        // POST: api/CashPaid
        [Route("api/CashPaid")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostCashPaid(int orderId)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Orders getorder = db.Orders.Find(orderId);
            if (getorder.paid == false)
            {
                getorder.paid = true;
                await db.SaveChangesAsync();
                return Ok(getorder);
            }
            else
            {
                return Ok("Its already paid for");
            }
        }
        // POST: api/foodServed
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/FoodServed")]
        [ResponseType(typeof(MenuBindingModels.FoodServed))]
        public async Task<IHttpActionResult> FoodServed(MenuBindingModels.FoodServed fo)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FoodOrdered getfood = db.food_ordered.Find(fo.food_ordered_id);
            getfood.served=true;

            db.SaveChanges();
            var results = from foodordered in db.food_ordered
                          where (fo.order_id == foodordered.order_id)
                          select new MenuBindingModels.Foodserved
                          {
                             served=foodordered.served
                          };

            var OrderedFoodList = await results.ToListAsync();
            var checker = false;
            foreach(var i in OrderedFoodList)
            {
                if (i.served == false)
                {
                    checker = true;
                }
            }
            if (checker == false)
            {
                var ordercomplete = db.Orders.Find(fo.order_id);
                ordercomplete.completed = true;
                db.SaveChanges();

            }

            return Ok(fo);
        }

        // POST: api/foodServed
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/AllFoodServed")]
        [ResponseType(typeof(FoodOrdered))]
        public async Task<IHttpActionResult> AllFoodServed(int order_id)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = from foodordered in db.food_ordered
                          where (order_id == foodordered.order_id)
                          select new MenuBindingModels.Foodserved
                          {
                              fdId= foodordered.food_ordered_id,
                              served = foodordered.served
                          };
             
            var OrderedFoodList = await results.ToListAsync();
            foreach (var i in OrderedFoodList)
            {
                if(i.served == false)
                {
                    FoodOrdered food = db.food_ordered.Find(i.fdId);
                    food.served = true;
                    db.SaveChanges();
                }
            }
            Orders ordercomplete = db.Orders.Find(order_id);
            ordercomplete.completed = true;
            db.SaveChanges();

            return Ok("Order completed");
        }

        // POST: api/SendOrder
        [Route("api/SendOrder")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostSendOrders(Orders orders)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            orders.order_time = DateTime.Now.AddHours(8);
            db.Orders.Add(orders);
            await db.SaveChangesAsync();

            return Ok(orders);
        }

        // POST: api/addTableNum
        [Route("api/addTableNum")]
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostAddTableNum(MenuBindingModels.TableUpdate updateinfo)
        {
            //add to order

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Orders retrieveOrder=db.Orders.Find(updateinfo.order_id);
            if (retrieveOrder != null)
            {
                retrieveOrder.table_id = updateinfo.table_id;
                await db.SaveChangesAsync();
            }

            return Ok("Updated : "+ updateinfo.order_id + retrieveOrder);
        }
        // POST: api/PostOutletReport/
        [Route("api/PostOutletReport")]
        [ResponseType(typeof(MenuBindingModels.GetOrderedItems))]
        public async Task<IHttpActionResult> PostOutletReport(MenuBindingModels.PostGetReport dateRange, int outletID)
        {
            DateTime sd = DateTime.ParseExact(dateRange.startdate, "dd/MM/yyyy", null);
            DateTime ed = DateTime.ParseExact(dateRange.enddate, "dd/MM/yyyy", null);
            var results = from o in db.Orders
                          join rt in db.RestaurantTable on o.table_id equals rt.table_id
                          join ol in db.Outlets on rt.outlet_id equals ol.outlet_id
                          join m in db.Merchants on ol.merchant_id equals m.merchant_id
                          where ol.outlet_id == outletID
                          where DateTime.Compare(o.order_time, sd) >= 0
                          where DateTime.Compare(o.order_time, ed) <= 0
                          select new MenuBindingModels.GetReport
                          {
                              order_id = o.order_id,
                              order_bill = o.total_bill,
                              order_time = o.order_time,
                              order_status = o.completed,
                              outlet_id = ol.outlet_id,
                              outlet_name = ol.name,
                              order_payment = o.paid,
                              merchant_id = m.merchant_id,
                              merchantname = m.biz_name,
                              merchantdp = m.merchant_photo
                          };

            var OrdersList = await results.ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(OrdersList);
        }

        // POST: api/Orders
        [ResponseType(typeof(Orders))]
        public async Task<IHttpActionResult> PostOrders(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Orders.Add(orders);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orders.order_id }, orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.order_id == id) > 0;
        }
    }
}