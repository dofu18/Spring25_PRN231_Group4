using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage_FE.Pages.OrderPage;
using System.Text.Json;

namespace RazorPage_FE.Pages.Course
{
    public class CourseModel : PageModel
    {
        public List<Course> Courses { get; set; } = new();
        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7139");
                var response = await client.GetAsync("/v1/Course");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Courses = System.Text.Json.JsonSerializer.Deserialize<List<Course>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
        }
        
    }

    public class Course
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Discount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CourseDetail { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Metadata { get; set; } = string.Empty;
        public float AvgRating { get; set; }
        public User Tutor { get; set; }
    }

    public class User()
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
