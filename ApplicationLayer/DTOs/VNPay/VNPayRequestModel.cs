using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.VNPay
{
    public class VNPayRequestModel
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Description {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
