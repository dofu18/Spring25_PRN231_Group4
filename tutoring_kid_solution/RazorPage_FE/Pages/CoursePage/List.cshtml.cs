using Application.RespType;
using ApplicationLayer.DTOs.Course;
using ApplicationLayer.Services.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage_FE.Pages.CoursePage
{
    public class ListModel : PageModel
    {
        private readonly ICourseService _courseService;

        public ListModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseResponseModel> Courses { get; set; } = new();
        public PagingMetaData PageInfo { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchKeyword { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNum { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 5;

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _courseService.GetAllCoursesAsync(new GetAllCourseDto
            {
                pageNum = PageNum,
                pageSize = PageSize,
                keyWord = SearchKeyword,
                Status = Status
            });

            if (response.Code == 200 && response.Data != null)
            {
                Courses = response.Data.PageData;
                PageInfo = response.Data.PageInfo;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error fetching courses.");
            }

            return Page();
        }
    }
}
