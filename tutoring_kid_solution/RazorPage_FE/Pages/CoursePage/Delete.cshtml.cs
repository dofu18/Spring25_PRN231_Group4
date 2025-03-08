using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage_FE.Pages.CoursePage
{
    public class DeletePage(ICourseService courseService) : PageModel
    {
        private readonly ICourseService _courseService = courseService;
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CourseResponseModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _courseService.GetCourseByIdAsync(id);
            if (response.Code == 404 || response.Data == null)
            {
                return NotFound();
            }

            Course = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsyn(Guid id)
        {
            var response = await _courseService.DeleteCourseAsync(id, false); 

            if (response.Code == 404)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
