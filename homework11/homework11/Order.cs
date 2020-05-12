using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework11
{
    class Order
    {
        public int OrderId { get; set; }
        public String Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
