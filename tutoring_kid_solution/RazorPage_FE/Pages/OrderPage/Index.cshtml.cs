using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage_FE.Pages.OrderPage
{
    public class OrderHistoryModel : PageModel
    {
        public List<OrderCourse> OrderCourses { get; set; } = new();

        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7139");
                var response = await client.GetAsync("/v1/OrderCourse");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    OrderCourses = System.Text.Json.JsonSerializer.Deserialize<List<OrderCourse>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }

    public class OrderCourse
    {
        public Guid CourseId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public Course Course { get; set; } = new();
        public Order Order { get; set; } = new();
    }

    public class Course
    {
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; }
    }

    public class Order
    {
        public string Status { get; set; } = "Pending";
    }
}

