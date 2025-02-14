using ApplicationLayer.Services.OrderCourses;
using ApplicationLayer.Services.Orders;
using DomainLayer.Constants;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route(Constants.Http.API_VERSION + "/OrderCourse")]
    public class OrderCourseController : ControllerBase
    {
        private readonly IOrderCourseService _orderCourseService;
        private ILogger<OrderCourseController> _logger;

        public OrderCourseController(ILogger<OrderCourseController> logger, IOrderCourseService orderCourseService)
        {
            _logger = logger;
            _orderCourseService = orderCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderCourse([FromQuery] Guid? userId)
        {
            var orderCourseList = (await _orderCourseService.List(userId)).ToList();

            //var listObj = new List<object>();
            //foreach (var projectRecruit in projectRecruitList)
            //{
            //    var projectRecruitRoleList = (await _projectRecruitRolesService.SearchByProjectRecruitId(projectRecruit.Id)).ToList();
            //    var response = new
            //    {
            //        projectRecruit,
            //        projectRecruitRoleList,
            //    };
            //    listObj.Add(response);
            //}
            return Ok(orderCourseList);
        }
    }
}
