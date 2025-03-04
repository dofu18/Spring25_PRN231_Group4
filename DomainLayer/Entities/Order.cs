using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Order : BaseEntity
    {
        public float TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }

        // Navigation Property
        public User CreatedUser { get; set; } 
    }

}
