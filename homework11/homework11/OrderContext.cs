﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework11
{
    class OrderContext: DbContext
    {
        public OrderContext() : base("OrderDataBase") {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<OrderContext>()) ;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
