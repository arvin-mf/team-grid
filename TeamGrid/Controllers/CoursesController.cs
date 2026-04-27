using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseServ;

    public CoursesController(ICourseService courseServ)
    {
        _courseServ = courseServ;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest req)
    {
        var courseId = await _courseServ.Create(req);

        return Created("", new ApiSuccessResponse<int>(courseId, "Course successfully created"));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseServ.GetAll();

        return Ok(new ApiSuccessResponse<List<CourseDto>>(courses, "Courses successfully retrieved"));
    }
}