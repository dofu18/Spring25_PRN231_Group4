using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RazorPage_FE.Pages.CoursePage
{
    public class CreateModel(ICourseService courseService) : PageModel
    {
        private readonly ICourseService _courseService = courseService;
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CourseCreateDto Course { get; set; } = new CourseCreateDto();

        public async Task<IActionResult> OnPostAsyn()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            var response = await _courseService.CreateCourseAsync(Course);

            if (response.Code == 201) 
            {
                return RedirectToPage("Index"); 
            }

            ModelState.AddModelError(string.Empty, "Failed to create course. Please try again.");
            return Page();
        }
    }
}
