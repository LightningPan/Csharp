using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework11
{
    class OrderItem
    {
        [Key]
        public int Index { get; set; }   
        public String GoodsName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
