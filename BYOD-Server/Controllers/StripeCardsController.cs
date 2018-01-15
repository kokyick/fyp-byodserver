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
using System.Globalization;
using Stripe;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;

namespace BYOD_Server.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StripeCardsController : ApiController
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
        // GET: api/StripeCards
        public IQueryable<StripeCards> GetStripeCards()
        {
            return db.StripeCards;
        }

        // GET: api/StripeCards/5
        [ResponseType(typeof(StripeCards))]
        public async Task<IHttpActionResult> GetStripeCards(int id)
        {
            StripeCards stripeCards = await db.StripeCards.FindAsync(id);
            if (stripeCards == null)
            {
                return NotFound();
            }

            return Ok(stripeCards);
        }

        // PUT: api/StripeCards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStripeCards(int id, StripeCards stripeCards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stripeCards.Card_id)
            {
                return BadRequest();
            }

            db.Entry(stripeCards).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StripeCardsExists(id))
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

        //// POST: api/StripeCards
        //[ResponseType(typeof(StripeCards))]
        //public async Task<IHttpActionResult> PostStripeCards(StripeCards stripeCards)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.StripeCards.Add(stripeCards);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = stripeCards.card_id }, stripeCards);
        //}

        // DELETE: api/StripeCards/5
        [ResponseType(typeof(StripeCards))]
        public async Task<IHttpActionResult> DeleteStripeCards(int id)
        {
            StripeCards stripeCards = await db.StripeCards.FindAsync(id);
            if (stripeCards == null)
            {
                return NotFound();
            }

            db.StripeCards.Remove(stripeCards);
            await db.SaveChangesAsync();

            return Ok(stripeCards);
        }

        // POST: api/StripeCards
        [Route("api/GetStripeCards")]
        [ResponseType(typeof(StripeCards))]
        public async Task<IHttpActionResult> GetStripeCards(CardModel card)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var stripeCards = await db.StripeCards.Where(m => m.User_id == user.Id).ToListAsync();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(stripeCards);
        }
        // POST: api/StripeCards
        [Route("api/GetCurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return Ok(user.Id);
        }

        // POST: api/StripeCards
        [Route("api/AddStripeCards")]
        [ResponseType(typeof(StripeCards))]
        public async Task<IHttpActionResult> PostAddStripeCards(CardModel card)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //// Set your secret key: remember to change this to your live secret key in production
            //// See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_OWXcF1GJ4STCWBuu0bade9oq");

            //// Token is created using Checkout or Elements!
            ////Get the payment token submitted by the form:
            var token = card.Token; // Using ASP.NET MVC

            var customers = new StripeCustomerService();
            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = user.UserName,
                SourceToken = token
            });
            //var cardtype = 0;
            //if (card.CardType.ToLower() == "visa")
            //    cardtype = 1;
            //else if (card.CardType.ToLower() == "master")
            //    cardtype = 2;
            //else if (card.CardType.ToLower() == "amex")
            //    cardtype = 3;
            var last4num= card.CardNumber.Substring(Math.Max(0, card.CardNumber.Length - 4));
            StripeCards stripeCards = new StripeCards
            {
                Last_four_digit = last4num,
                Payment_key = customer.Id,
                User_id = user.Id,
                Card_type_id = card.CardType
            };
            ////    // YOUR CODE: Save the customer ID and other info in a database for later.
            db.StripeCards.Add(stripeCards);
            await db.SaveChangesAsync();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(stripeCards);
        }
        // POST: api/StripeCards
        [Route("api/ChargeStripeCards")]
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> PostChargeStripeCards(CardBindingModels.CardPayment card, int amt)
        {
            //ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_OWXcF1GJ4STCWBuu0bade9oq");

            var charges = new StripeChargeService();
            // YOUR CODE (LATER): When it's time to charge the customer again, retrieve the customer ID.
            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = amt, // $15.00 this time
                Currency = "myr",
                CustomerId = card.Payment_key
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //add to payment history
            var payment = new PaymentHistory {
                amouunt = amt,
                payment_time = DateTime.Now,
                remarks = "food",
                //user_id=user.Id,
                user_id = card.User_id,
                card_id=card.Card_id
            };
            db.PaymentHistories.Add(payment);
            await db.SaveChangesAsync();

            Orders ord=db.Orders.Find(card.Order_ID);
            ord.paid = true;
            await db.SaveChangesAsync();

            return Ok(payment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StripeCardsExists(int id)
        {
            return db.StripeCards.Count(e => e.Card_id == id) > 0;
        }
    }
}