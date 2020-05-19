using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderApp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace homework12.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderContext OrderDatabase;
        public OrderController(OrderContext context) {
            this.OrderDatabase = context;
        }
        // GET: api/<controller>
      

        [HttpGet]
        public ActionResult<List<Order>> GetOrder(string goodsName,string customerName,float? totalAmount) {
            IQueryable<Order> query = OrderDatabase.Orders;
            if (goodsName != null) { 
                query=query.Where(o => o.Items.Count(i => i.GoodsItem.Name == goodsName) > 0);
            }
            if (customerName != null) { 
                query=query.Where(o => o.Customer.Name == customerName); 
            }
            if (totalAmount != null) {
                query = query.Where(o => o.Items.Sum(item => item.GoodsItem.Price * item.Quantity) > totalAmount);
            }
            return query.ToList();
        
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(string id)
        {
            var order = OrderDatabase.Orders.FirstOrDefault(o => o.Id == id);
            if (order==null) {
                return NotFound();
            }
            return order;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {
            try
            {
                OrderDatabase.Orders.Add(order);
                OrderDatabase.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return order;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(string id,Order order)
        {
            if (id != order.Id) { 
                return BadRequest("Id cannot be modified");
            }
            try {
                OrderDatabase.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                OrderDatabase.SaveChanges();
            }
            catch (Exception e) {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try {
                var order = OrderDatabase.Orders.FirstOrDefault(t => t.Id == id);
                if (order != null) {
                    OrderDatabase.Remove(order);
                    OrderDatabase.SaveChanges();
                }
            }
            catch (Exception e) {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }
    }
}
