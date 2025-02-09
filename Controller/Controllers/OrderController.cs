using ApplicationLayer.Services.Orders;
using DomainLayer.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route(Constants.Http.API_VERSION + "/Order")]
    public class OrderController
    {
        private readonly IOrderService _orderService;
        private ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }


    }
}
