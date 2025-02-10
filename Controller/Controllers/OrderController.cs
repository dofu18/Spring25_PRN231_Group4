using ApplicationLayer.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            _logger.LogInformation("Create order request received");

            return await _orderService.Create(dto);
        }
    }
}
