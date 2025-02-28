using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.VNPay;
using Microsoft.AspNetCore.Http;

namespace ApplicationLayer.Services.VNPay
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(HttpContext context, VNPayRequestModel model);
        VNPayResponseModel PaymentExecute(IQueryCollection collections);
    }
}
