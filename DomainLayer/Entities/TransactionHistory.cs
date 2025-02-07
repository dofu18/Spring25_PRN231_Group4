using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class TransactionHistory : BaseEntity
    {
        public float Amount { get; set; }
        public string Message { get; set; } = string.Empty;

        // Navigation Property
        public virtual User User { get; set; }
    }

}
