using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class OrderCreateDto
    {
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
