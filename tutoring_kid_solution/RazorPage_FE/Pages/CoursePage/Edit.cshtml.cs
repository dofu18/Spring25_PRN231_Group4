using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage_FE.Pages.CoursePage
{
    public class EditModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseCreateDto Course { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var response = await _courseService.GetCourseByIdAsync(id);

            if (response.Code == 200 && response.Data != null)
            {
                Course = new CourseCreateDto
                {
                    Name = response.Data.Name,
                    Description = response.Data.Description
                };
                return Page();
            }
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _courseService.UpdateCourseAsync(Course, id);

            if (response.Code == 200)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return Page();
            }
        }
    }
}
