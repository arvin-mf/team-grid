public interface ICourseService
{
    Task<int> Create(CreateCourseRequest req);
    Task<List<CourseDto>> GetAll();
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;

    public CourseService(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Create(CreateCourseRequest req)
    {
        var course = req.ToEntity();

        return await _repo.Insert(course);
    }

    public async Task<List<CourseDto>> GetAll()
    {
        var courses = await _repo.FindAll();

        return courses.Select(c => c.ToDto()).ToList();
    }
}