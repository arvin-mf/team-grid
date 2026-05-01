using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseServ;
    private readonly IClassService _classServ;

    public CoursesController(ICourseService courseServ, IClassService classServ)
    {
        _courseServ = courseServ;
        _classServ = classServ;
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

    [HttpPatch("{id}/name")]
    public async Task<IActionResult> SetCourseName(int id, [FromBody] SetCourseNameRequest req)
    {
        await _courseServ.SetName(id, req);

        return Ok(new ApiSuccessResponse<object>(null, "Course successfully updated"));
    }

    [HttpPost("{id}/classes")]
    public async Task<IActionResult> CreateClass(int id, [FromBody] CreateClassRequest req)
    {
        var classId = await _classServ.Create(id, req);

        return Created("", new ApiSuccessResponse<int>(classId, "Class successfully created"));
    }

    [HttpGet("{id}/classes")]
    public async Task<IActionResult> GetClassesByCourse(int id)
    {
        var classes = await _classServ.GetByCourseId(id);

        return Ok(new ApiSuccessResponse<List<ClassDto>>(classes, "Classes successfully retrieved"));
    }

    [HttpGet("{id}/schedule")]
    public async Task<IActionResult> GenerateSchedule(int id, int max_persession)
    {
        var schedule = await _courseServ.GenerateSchedule(id, max_persession);

        return Ok(new ApiSuccessResponse<ScheduleDto>(schedule, "Schedule successfully generated"));
    }
}