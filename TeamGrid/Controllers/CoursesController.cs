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
}