using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage_FE.Pages.CoursePage
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DetailsModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseResponseModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _courseService.GetCourseByIdAsync(id);

            if (response.Code == 200 && response.Data != null)
            {
                Course = response.Data;
                return Page();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
