using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp {
  public class OrderContext : DbContext {

    public OrderContext(DbContextOptions<OrderContext> options) : base(options) {
            this.Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Goods> GoodItems { get; set; }
    public DbSet<Customer> Customers{ get; set; }

  }
}
